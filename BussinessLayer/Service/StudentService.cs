using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Parents;
using DataAccessLayer.DTO.Students;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BussinessLayer.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentrepo;
        private readonly IClassRoomRepository _classroomrepo;
        private readonly IParentRepository _parentrepo;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepo studentrepo, IClassRoomRepository classRoomRepository,
            IParentRepository parentRepository, IMapper mapper)
        {
            _studentrepo = studentrepo;
            _classroomrepo = classRoomRepository;
            _parentrepo = parentRepository;
            _mapper = mapper;
        }

        public async Task<Student> AddStudentAsync(AddStudentDTO student)
        {
            Student addedstudent = _mapper.Map<Student>(student);
            await _studentrepo.AddAsync(addedstudent);
            return addedstudent;
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _studentrepo.GetByIdAsync(id);
            if (student != null)
            {
                student.IsDeleted = true;
                _studentrepo.Save();
            }
        }

        public async Task<List<StudentDTO>> GetAllStudentsAsync()
        {
            try
            {
                var list = await _studentrepo.GetAllAsync();
                List<StudentDTO> returnlist = new List<StudentDTO>();
                foreach (var student in list)
                {
                    var parent = await _parentrepo.GetByIdAsync(student.Parentid);
                    var listparent = _mapper.Map<ParentStudent>(parent);
                    var studentDTO = _mapper.Map<StudentDTO>(student);
                    studentDTO.parent = listparent;
                    returnlist.Add(studentDTO);
                }
                return returnlist;
            }
            catch(Exception e)
            {
                throw new Exception($"Error retrieving students: {e.Message}", e);
            }

        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _studentrepo.GetByIdAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found.");
            }
            var studentdto = _mapper.Map<StudentDTO>(student);
            studentdto.parent = _mapper.Map<ParentStudent>(await _parentrepo.GetByIdAsync(student.Parentid));
            studentdto.Classname = (await _classroomrepo.GetByIdAsync(student.Classid)).Classname;
            return studentdto;
        }

        public async Task<List<StudentDTO>> GetStudentByParentId(int parentId)
        {
            var students = await _studentrepo.GetAllAsync();
            var filteredStudents = students.Where(s => s.Parentid == parentId && !s.IsDeleted).ToList();
            var returnlist = new List<StudentDTO>();

            foreach (var student in filteredStudents)
            {
                var parent = await _parentrepo.GetByIdAsync(student.Parentid);
                var parentdto = _mapper.Map<ParentStudent>(parent);
                var studentDTO = _mapper.Map<StudentDTO>(student);
                studentDTO.parent = parentdto;
                studentDTO.Classname = _classroomrepo.GetByIdAsync(student.Classid).Result.Classname;
                returnlist.Add(studentDTO);
            }
            return returnlist;
        }

        public async Task<Student> UpdateStudentAsync(UpdateStudentDTo student)
        {
            var s = await _studentrepo.GetByIdAsync(student.Id);
            if (s == null)
                return null;

            s.StudentCode = student.StudentCode;
            s.Fullname = student.Fullname;
            s.Age = student.Age;
            s.BloodType = student.BloodType;
            s.Gender = student.Gender;
            s.Dob = student.Dob;
            s.Classid = student.Classid;
            s.Parentid = student.Parentid;
            s.UpdatedAt = DateTime.Now;
            _studentrepo.Update(s);
            await _studentrepo.SaveChangesAsync();
            return s;
        }

        public async Task<string> UploadStudentList(List<InsertStudent> studentlist)
        {
            try
            {
                var parentlist = await _parentrepo.GetAllAsync();
                var classlist = await _classroomrepo.GetAllAsync();
                var students = await _studentrepo.GetAllAsync();
                string errorMessage = string.Empty;

                foreach (var student in studentlist)
                {
                    try
                    {
                        if (student != null)
                        {
                            if (students.FirstOrDefault(s => s.StudentCode == student.studentCode) == null)

                            {
                                Classroom classroom = classlist.FirstOrDefault(c => c.Classname == student.className);
                                Parent parent = parentlist.FirstOrDefault(p => p.Fullname == student.parentName && p.Phone == student.parentphone);
                                if (parent != null && classroom != null)
                                {
                                    AddStudentDTO addstudent = new()
                                    {
                                        Fullname = student.fullName,
                                        Age = DateTime.Now.Year - student.birthDate.Year -
                                              (DateTime.Now.DayOfYear < student.birthDate.DayOfYear ? 1 : 0),
                                        BloodType = student.bloodtype,
                                        Classid = classroom.Classid,
                                        Parentid = parent.Parentid,
                                        Dob = student.birthDate,
                                        Gender = student.gender == "Nam" ? true : student.gender == "Nữ" ? false : throw new ArgumentException("Invalid gender value"),
                                        StudentCode = student.studentCode,
                                    };
                                    Student newstudent = await AddStudentAsync(addstudent);
                                    classroom.Students.Add(newstudent);
                                    parent.Students.Add(newstudent);
                                }
                                else
                                {
                                    errorMessage += ($"Parent or Classroom not found for student: {student.fullName} - {student.className} - {student.parentName} - {student.parentphone}\n");
                                }
                            }

                            else errorMessage += ($"Student already exist: {student.fullName} - {student.className} - {student.parentName} - {student.parentphone}\n");

                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage += ($"Error processing student {student.fullName}: {ex.Message}\n");
                    }
                }
                _classroomrepo.Save();
                _parentrepo.Save();
                _studentrepo.Save();
                if (errorMessage != string.Empty)
                {
                    throw new InvalidDataException(errorMessage);
                }
                return "Student insert successfully";
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Error uploading student list: {ex.Message}", ex);
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error uploading student list: {e.Message}", e);
            }
        }
        public (List<InsertStudent>, string) ProcessExcelFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (null, "Không có file");

            if (file.Length > 10 * 1024 * 1024) // 10MB
                return (null, "File quá lớn. Kích thước tối đa 10MB");

            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return (null, "Định dạng file không hợp lệ. Chỉ chấp nhận .xlsx, .xls");
            //Kiểm tra file

            var students = new List<InsertStudent>();
            using var stream = file.OpenReadStream();
            IWorkbook workbook;
            if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                workbook = new XSSFWorkbook(stream);
            else
                workbook = new HSSFWorkbook(stream);

            //check file sheet
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null || row.Cells.All(c => c.CellType == CellType.Blank))
                    continue; // Skip empty rows
                try
                {
                    DateTime date = ParseDateValue(GetCellStringValue(row, 6));
                    var student = new InsertStudent
                    {
                        studentCode = GetCellStringValue(row, 0).Trim(),
                        fullName = GetCellStringValue(row, 1).Trim(),
                        bloodtype = GetCellStringValue(row, 2).Trim(),
                        className = GetCellStringValue(row, 3).Trim(),
                        parentName = GetCellStringValue(row, 4).Trim(),
                        parentphone = GetCellStringValue(row, 5).Trim(),

                        birthDate = DateOnly.FromDateTime(date),
                        gender = GetCellStringValue(row, 7).Trim(),
                        healthStatus = GetCellStringValue(row, 8).Trim()

                    };
                    students.Add(student);
                }
                catch (Exception ex) { Console.WriteLine($"Error processing row {i + 1}: {ex.Message}"); }
            }
            return (students, "Student insert successfully");
        }
        private string GetCellStringValue(IRow row, int cellIndex)
        {
            var cell = row.GetCell(cellIndex);
            if (cell == null) return "";

            return cell.CellType switch
            {
                CellType.String => cell.StringCellValue,
                CellType.Numeric => cell.NumericCellValue.ToString(),
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                CellType.Formula => cell.StringCellValue,
                _ => ""
            };
        }
        private DateTime ParseDateValue(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                throw new ArgumentException("Ngày sinh không được để trống");

            var formats = new[] {
            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
            "yyyy-MM-dd", "MM/dd/yyyy", "M/d/yyyy"
        };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    return result;
            }

            throw new ArgumentException($"Định dạng ngày không hợp lệ: {dateString}");
        }

        private int ParseIntValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Lớp không được để trống");

            if (!int.TryParse(value, out int result))
                throw new ArgumentException($"Lớp phải là số: {value}");

            return result;
        }
    }
}

