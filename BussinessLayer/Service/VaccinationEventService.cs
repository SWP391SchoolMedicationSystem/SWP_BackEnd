using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NPOI.POIFS.Properties;
using Scriban;

namespace BussinessLayer.Service
{
    public class VaccinationEventService : IVaccinationEventService
    {
        private readonly IVaccinationEventRepository _vaccinationEventRepository;
        private readonly IVaccinationRecordRepository _vaccinationRecordRepository;
        private readonly IParentService _parentService;
        private readonly IEmailService _emailService;
        private readonly IEmailRepo _emailRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileHandler _fileHandler;

        public VaccinationEventService(
            IVaccinationEventRepository vaccinationEventRepository,
            IVaccinationRecordRepository vaccinationRecordRepository,
            IEmailService emailService,
            IEmailRepo emailRepo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IParentService parentService,
            FileHandler fileHandler)
        {
            _vaccinationEventRepository = vaccinationEventRepository;
            _vaccinationRecordRepository = vaccinationRecordRepository;
            _emailService = emailService;
            _emailRepo = emailRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _parentService = parentService;
            _fileHandler = fileHandler;
        }

        public async Task<List<VaccinationEventDTO>> GetAllEventsAsync()
        {
            var events = await _vaccinationEventRepository.GetAllActiveEventsAsync();
            var eventDtos = _mapper.Map<List<VaccinationEventDTO>>(events);

            // Add statistics to each event
            foreach (var eventDto in eventDtos)
            {
                var stats = await GetEventStatisticsAsync(eventDto.VaccinationEventId);
                eventDto.ConfirmedCount = stats["Confirmed"];
                eventDto.DeclinedCount = stats["Declined"];
                eventDto.PendingCount = stats["Pending"];
                eventDto.TotalStudents = stats["Total"];
            }

            return eventDtos;
        }

        public async Task<VaccinationEventDTO> GetEventByAccessToken(string accessToken)
        {
            var vaccinationEvent = await _vaccinationEventRepository.GetEventByAccessTokenAsync(accessToken);
            if (vaccinationEvent == null)
                return null;

            var eventDto = _mapper.Map<VaccinationEventDTO>(vaccinationEvent);
            var stats = await GetEventStatisticsAsync(vaccinationEvent.Vaccinationeventid);
            eventDto.ConfirmedCount = stats["Confirmed"];
            eventDto.DeclinedCount = stats["Declined"];
            eventDto.PendingCount = stats["Pending"];
            eventDto.TotalStudents = stats["Total"];
            return eventDto;
        }

        public async Task<VaccinationEventDTO?> GetEventByIdAsync(int eventId)
        {
            var vaccinationEvent = await _vaccinationEventRepository.GetEventWithRecordsAsync(eventId);
            if (vaccinationEvent == null)
                return null;

            var eventDto = _mapper.Map<VaccinationEventDTO>(vaccinationEvent);
            var stats = await GetEventStatisticsAsync(eventId);
            eventDto.ConfirmedCount = stats["Confirmed"];
            eventDto.DeclinedCount = stats["Declined"];
            eventDto.PendingCount = stats["Pending"];
            eventDto.TotalStudents = stats["Total"];

            return eventDto;
        }

        public async Task<VaccinationEventDTO> CreateEventAsync(CreateVaccinationEventDTO dto,string storedFileName, string createdBy)
        {
            string? accessToken = null;

            if (!storedFileName.IsNullOrEmpty())
            {
                accessToken = Guid.NewGuid().ToString();
            }

            var vaccinationEvent = new Vaccinationevent
            {
                Vaccinationeventname = dto.VaccinationEventName,
                Location = dto.Location,
                Organizedby = dto.OrganizedBy,
                Eventdate = dto.EventDate,
                Description = dto.Description,
                Createddate = DateTime.Now,
                Modifieddate = DateTime.Now,
                Createdby = createdBy,
                Modifiedby = createdBy,
                Isdeleted = false,
                Documentfilename = storedFileName,
                Documentaccesstoken = accessToken
            };

            try
            {
                await _vaccinationEventRepository.AddAsync(vaccinationEvent);
                await _vaccinationEventRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can use a logging framework like Serilog, NLog, etc.
                throw new InvalidOperationException("An error occurred while creating the vaccination event.", ex);
            }

            return _mapper.Map<VaccinationEventDTO>(vaccinationEvent);
        }

        public async Task<VaccinationEventDTO> UpdateEventAsync(UpdateVaccinationEventDTO dto, string modifiedBy)
        {
            var existingEvent = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
            if (existingEvent == null)
                throw new InvalidOperationException("Vaccination event not found.");

            string? oldFileName = existingEvent.Documentfilename;

            // Case A: A new file is uploaded. This means we ADD or REPLACE.
            if (dto.DocumentFile != null)
            {
                // First, delete the old file if one existed
                _fileHandler.Delete(oldFileName);

                // Then, upload the new file
                var uploadResult = await _fileHandler.UploadAsync(dto.DocumentFile);
                if (!uploadResult.Success)
                {
                    return null;//BadRequest(uploadResult.ErrorMessage);
                }

                // Update the database record with the NEW file info and a NEW access token
                existingEvent.Documentfilename = uploadResult.StoredFileName;
                existingEvent.Documentaccesstoken = Guid.NewGuid().ToString();
            }
            // Case B: No new file is uploaded, but the user explicitly wants to REMOVE the existing file.
            else if (dto.DocumentDelete)
            {
                // Delete the file from the disk
                _fileHandler.Delete(oldFileName);

                // Clear the file info in the database record
                existingEvent.Documentfilename = null;
                existingEvent.Documentaccesstoken = null;
            }
            // Case C: No new file is uploaded and RemoveDocument is false.
            // We do nothing to the file fields. The existing file remains untouched.

            existingEvent.Vaccinationeventname = dto.VaccinationEventName;
            existingEvent.Location = dto.Location;
            existingEvent.Organizedby = dto.OrganizedBy;
            existingEvent.Eventdate = dto.EventDate;
            existingEvent.Description = dto.Description;
            existingEvent.Modifieddate = DateTime.Now;
            existingEvent.Modifiedby = modifiedBy;

            _vaccinationEventRepository.Update(existingEvent);
            await _vaccinationEventRepository.SaveChangesAsync();

            return _mapper.Map<VaccinationEventDTO>(existingEvent);
        }

        public async Task<bool> DeleteEventAsync(int eventId, string deletedBy)
        {
            var existingEvent = await _vaccinationEventRepository.GetByIdAsync(eventId);
            if (existingEvent == null)
                return false;

            existingEvent.Isdeleted = true;
            existingEvent.Modifieddate = DateTime.Now;
            existingEvent.Modifiedby = deletedBy;

            string existingFileName = existingEvent.Documentfilename;

            if(existingFileName.IsNullOrEmpty())
                // Delete the file from the disk
                _fileHandler.Delete(existingFileName);

            _vaccinationEventRepository.Update(existingEvent);
            await _vaccinationEventRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<VaccinationEventDTO>> GetUpcomingEventsAsync()
        {
            var events = await _vaccinationEventRepository.GetUpcomingEventsAsync();
            return _mapper.Map<List<VaccinationEventDTO>>(events);
        }

        public async Task<List<VaccinationEventDTO>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var events = await _vaccinationEventRepository.GetEventsByDateRangeAsync(startDate, endDate);
            return _mapper.Map<List<VaccinationEventDTO>>(events);
        }

        public async Task<VaccinationEventSummaryDTO> GetEventSummaryAsync(int eventId)
        {
            return await _vaccinationEventRepository.GetEventSummaryAsync(eventId);
        }

        public async Task<List<StudentVaccinationStatusDTO>> GetStudentResponsesForEventAsync(int eventId)
        {
            return await _vaccinationEventRepository.GetStudentResponsesForEventAsync(eventId);
        }
        //=====================================================
        public async Task<List<EmailDTO>> SendVaccinationEmailToAllParentsAsync(SendVaccinationEmailDTO dto, string baseUrl)
        {
            try
            {
                var eventInfo = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
                if (eventInfo == null)
                    return null;

                var emailTemplate = await _emailRepo.GetEmailTemplateByIdAsync(dto.EmailTemplateId);
                if (emailTemplate == null)
                    return null;

                // --- Step 1: Conditionally build the HTML for the detailed document link ---
                string detailedDocumentSectionHtml = ""; // Default to an empty string
                if (!string.IsNullOrWhiteSpace(eventInfo.Documentaccesstoken))
                {
                    string secureDownloadUrl = $"{baseUrl}/api/files/download/{eventInfo.Documentaccesstoken}";
                    // This is the full HTML block that will replace our placeholder
                    detailedDocumentSectionHtml = $@"
                            <div class='details-link'>
                                <a href='{secureDownloadUrl}' target='_blank' style='font-weight: bold; color: #0056b3; text-decoration: none;'>
                                    Tải về kế hoạch chi tiết của sự kiện
                                </a>
                            </div>";
                }

                var parents = await _vaccinationEventRepository.GetParentsForEventAsync(dto.VaccinationEventId);
                var parentsWithEmails = parents.Where(p => !string.IsNullOrEmpty(p.Email)).ToList();

                // Use the optimized bulk email method
                var failList = await _emailService.SendPersonalizedEmailsAsync(
                    parentsWithEmails,
                    dto.EmailTemplateId,
                    parent => new EmailDTO
                    {
                        To = parent.Email!,
                        Subject = emailTemplate.Subject,
                        Body = emailTemplate.Body
                            .Replace("{EventName}", eventInfo.Vaccinationeventname)
                            .Replace("{EventDate}", eventInfo.Eventdate.ToString("dd/MM/yyyy"))
                            .Replace("{Location}", eventInfo.Location)
                            .Replace("{Description}", eventInfo.Description)
                            .Replace("{CustomMessage}", dto.CustomMessage ?? "")
                            .Replace("{ResponseLink}", GenerateResponseLink(parent.Email!, dto.VaccinationEventId, null))
                            .Replace("{DetailedDocumentSection}", detailedDocumentSectionHtml)
                    },
                    batchSize: 20 // Process 20 emails per batch
                );

                return failList;
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can use a logging framework like Serilog, NLog, etc.
            }
            
            return new List<EmailDTO>();
        }

        public async Task<List<EmailDTO>> SendVaccinationEmailToSpecificParentsAsync(SendVaccinationEmailDTO dto, List<int> parentIds, string baseUrl)
        {
            try
            {
                var eventInfo = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
                if (eventInfo == null)
                    return null;

                var emailTemplate = await _emailRepo.GetEmailTemplateByIdAsync(dto.EmailTemplateId);
                if (emailTemplate == null)
                    return null;

                // --- Step 1: Conditionally build the HTML for the detailed document link ---
                string detailedDocumentSectionHtml = ""; // Default to an empty string
                if (!string.IsNullOrWhiteSpace(eventInfo.Documentaccesstoken))
                {
                    string secureDownloadUrl = $"{baseUrl}/api/files/download/{eventInfo.Documentaccesstoken}";
                    // This is the full HTML block that will replace our placeholder
                    detailedDocumentSectionHtml = $@"
                            <div class='details-link'>
                                <a href='{secureDownloadUrl}' target='_blank' style='font-weight: bold; color: #0056b3; text-decoration: none;'>
                                    Tải về kế hoạch chi tiết của sự kiện
                                </a>
                            </div>";
                }

                var parents = await _vaccinationEventRepository.GetParentsForEventAsync(dto.VaccinationEventId);
                var specificParents = parents.Where(p => parentIds.Contains(p.Parentid) && !string.IsNullOrEmpty(p.Email)).ToList();

                // Use the optimized bulk email method
                var failList = await _emailService.SendPersonalizedEmailsAsync(
                    specificParents,
                    dto.EmailTemplateId,
                    parent => new EmailDTO
                    {
                        To = parent.Email!,
                        Subject = emailTemplate.Subject,
                        Body = emailTemplate.Body
                            .Replace("{EventName}", eventInfo.Vaccinationeventname)
                            .Replace("{EventDate}", eventInfo.Eventdate.ToString("dd/MM/yyyy"))
                            .Replace("{Location}", eventInfo.Location)
                            .Replace("{Description}", eventInfo.Description)
                            .Replace("{ParentName}", parent.Fullname)
                            .Replace("{CustomMessage}", dto.CustomMessage ?? "")
                            .Replace("{ResponseLink}", GenerateResponseLink(parent.Email!, dto.VaccinationEventId, null))
                            .Replace("{DetailedDocumentSection}", detailedDocumentSectionHtml)
                    },
                    batchSize: 20 // Process 20 emails per batch
                );

                return failList;
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can use a logging framework like Serilog, NLog, etc.
            }

            return new List<EmailDTO>();
        }

        public async Task<List<EmailDTO>> SendVaccinationEmailToSpecificStudentsAsync(SendVaccinationEmailDTO dto, List<int> studentIds, string baseUrl)
        {
            try
            {
                var eventInfo = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
                if (eventInfo == null)
                    return null;
                var emailTemplate = await _emailRepo.GetEmailTemplateByIdAsync(dto.EmailTemplateId);
                if (emailTemplate == null)
                    return null;

                emailTemplate.Body = testTemplate;

                // --- Step 1: Conditionally build the HTML for the detailed document link ---
                string secureDownloadUrl = ""; // Default to an empty string
                if (!string.IsNullOrWhiteSpace(eventInfo.Documentaccesstoken))
                {
                    secureDownloadUrl = $"{baseUrl}/api/files/download/{eventInfo.Documentaccesstoken}";
                }

                var students = await _vaccinationEventRepository.GetStudentsForEventAsync(dto.VaccinationEventId);
                var specificStudents = students.Where(s => studentIds.Contains(s.Studentid) && s.Parent != null && !string.IsNullOrEmpty(s.Parent.Email)).ToList();

                var studentListHtml = new StringBuilder();
                foreach (var student in specificStudents)
                {
                    studentListHtml.Append($"<li style='margin-bottom: 5px;'><strong>{student.Fullname}</strong></li>");
                }

                // Use the optimized bulk email method
                var failList = await _emailService.SendPersonalizedEmailsAsync(
                    specificStudents,
                    dto.EmailTemplateId,
                    student => new EmailDTO
                    {
                        To = student.Parent!.Email!,
                        Subject = emailTemplate.Subject,
                        Body = emailTemplate.Body
                            .Replace("{EventName}", eventInfo.Vaccinationeventname)
                            .Replace("{EventDate}", eventInfo.Eventdate.ToString("dd/MM/yyyy"))
                            .Replace("{Location}", eventInfo.Location)
                            .Replace("{Description}", eventInfo.Description)
                            .Replace("{StudentName}", student.Fullname)
                            .Replace("{CustomMessage}", dto.CustomMessage ?? "")
                            .Replace("{ResponseLink}", GenerateResponseLink(student.Parent.Email!, dto.VaccinationEventId, specificStudents))
                            .Replace("{secureDownloadUrl}", secureDownloadUrl)
                            .Replace("{StudentList}", studentListHtml.ToString())
                    },
                    batchSize: 20 // Process 20 emails per batch
                );
                return failList;
            }
            catch (Exception ex)
            {
                // Log the exception
                // You can use a logging framework like Serilog, NLog, etc.
            }
            return new List<EmailDTO>();
        }

        public async Task<bool> ProcessParentResponseAsync(ParentVaccinationResponseDTO dto)
        {
            try
            {
                foreach (StudentVaccinationResponseDTO student in dto.Responses)
                {
                    // Check if record already exists
                    var existingRecord = await _vaccinationRecordRepository.GetRecordByStudentAndEventAsync(student.StudentId, dto.VaccinationEventId);

                    if (existingRecord != null)
                    {
                        // Update existing record
                        existingRecord.Willattend = student.WillAttend;
                        existingRecord.Reasonfordecline = student.ReasonForDecline;
                        existingRecord.Parentconsent = dto.ParentConsent;
                        existingRecord.Responsedate = DateTime.Now;
                        existingRecord.Updatedat = DateTime.Now;
                        existingRecord.Updatedby = dto.ParentId.ToString();
                        existingRecord.Confirmedbyparent = student.WillAttend;

                        _vaccinationRecordRepository.Update(existingRecord);
                        await _vaccinationRecordRepository.SaveChangesAsync();
                    }
                    else
                    {
                        // Create new record
                        var newRecord = new Vaccinationrecord
                        {
                            Studentid = student.StudentId,
                            Vaccinationeventid = dto.VaccinationEventId,
                            Vaccinename = "To be determined", // Will be set when actual vaccination occurs
                            Dosenumber = 1,
                            Vaccinationdate = DateOnly.FromDateTime(DateTime.Now), // Will be updated when actual vaccination occurs
                            Confirmedbyparent = student.WillAttend,
                            Willattend = student.WillAttend,
                            Reasonfordecline = student.ReasonForDecline,
                            Parentconsent = dto.ParentConsent,
                            Responsedate = DateTime.Now,
                            Isdeleted = false,
                            Createdat = DateTime.Now,
                            Updatedat = DateTime.Now,
                            Createdby = dto.ParentId.ToString(),
                            Updatedby = dto.ParentId.ToString()
                        };

                        await _vaccinationRecordRepository.AddAsync(newRecord);
                        await _vaccinationRecordRepository.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<List<ParentVaccinationResponseDTO>> GetParentResponsesForEventAsync(int eventId)
        {
            var records = await _vaccinationRecordRepository.GetRecordsByEventAsync(eventId);
            var responses = new List<ParentVaccinationResponseDTO>();

            foreach (var record in records)
            {
                responses.Add(new ParentVaccinationResponseDTO
                {
                    ParentId = record.Student.Parentid,
                    VaccinationEventId = record.Vaccinationeventid,
                    ParentConsent = record.Parentconsent ?? false,
                    Responses = new List<StudentVaccinationResponseDTO>
                    {
                        new StudentVaccinationResponseDTO
                        {
                            StudentId = record.Studentid,
                            WillAttend = record.Willattend ?? false,
                            ReasonForDecline = record.Reasonfordecline
                        }
                    }
                });
            }

            return responses;
        }

        public async Task<Dictionary<string, int>> GetEventStatisticsAsync(int eventId)
        {
            var confirmedCount = await _vaccinationRecordRepository.GetConfirmedCountAsync(eventId);
            var declinedCount = await _vaccinationRecordRepository.GetDeclinedCountAsync(eventId);
            var pendingCount = await _vaccinationRecordRepository.GetPendingCountAsync(eventId);
            var totalCount = confirmedCount + declinedCount + pendingCount;

            return new Dictionary<string, int>
            {
                ["Confirmed"] = confirmedCount,
                ["Declined"] = declinedCount,
                ["Pending"] = pendingCount,
                ["Total"] = totalCount
            };
        }

        public async Task<List<VaccinationEventDTO>> GetEventsWithStatisticsAsync()
        {
            var events = await _vaccinationEventRepository.GetAllActiveEventsAsync();
            var eventDtos = _mapper.Map<List<VaccinationEventDTO>>(events);

            foreach (var eventDto in eventDtos)
            {
                var stats = await GetEventStatisticsAsync(eventDto.VaccinationEventId);
                eventDto.ConfirmedCount = stats["Confirmed"];
                eventDto.DeclinedCount = stats["Declined"];
                eventDto.PendingCount = stats["Pending"];
                eventDto.TotalStudents = stats["Total"];
            }

            return eventDtos;
        }

        public async Task<List<StudentVaccinationStatusDTO>?> GetStudentByParentEmailAsync(string email, int eventId)
        {
            try
            {
                var students = await _vaccinationEventRepository.GetStudentsForEventAsync(eventId);
                var filterStudents = students.Where(s => s.Parent.Email.Equals(email));

                if (filterStudents == null)
                    throw new InvalidOperationException("No students found for the given parent email.");

                var list = new List<StudentVaccinationStatusDTO>();

                foreach (var student in filterStudents)
                {
                    list.Add(new StudentVaccinationStatusDTO
                    {
                        StudentId = student.Studentid,
                        StudentName = student.Fullname,
                        ParentName = student.Parent?.Fullname ?? "",
                        ParentEmail = student.Parent?.Email ?? "",
                        ClassName = student.Class?.Classname ?? "",
                        ParentId = student.Parentid
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }

        private string GenerateResponseLink(string email, int eventId, List<Student> students)
        {
            // This would typically generate a secure link to a web form
            // For now, we'll create a simple link structure
            var baseUrl = _httpContextAccessor.HttpContext?.Request.Scheme + "://" +
                         _httpContextAccessor.HttpContext?.Request.Host;

            var builder = new StringBuilder();
            builder.Append($"{baseUrl}/api/VaccinationEvent/Respond?");
            builder.Append($"email={Uri.EscapeDataString(email)}");
            builder.Append($"&eventId={eventId}");
            
            foreach(var student in students)
            {
                builder.Append($"&studentIds={student.Studentid}");
            }

            return builder.ToString();
        }

        public async Task<string> FillEmailTemplateData(string email, VaccinationEventDTO eventInfo, List<Student> students)
        {
            var emailTemplate = await _emailService.GetEmailByName(EmailTemplateKeys.VaccinationResponseEmail);
            var parent = await _parentService.GetParentByEmailForEvent(email);
            var scribanTemplate = Template.Parse(emailTemplate.Body);
            var studentList = students
                .Select(s => new
                {
                    name = s.Fullname,
                    id = s.Studentid
                }).ToList();

            string result = await scribanTemplate.RenderAsync(new
            {
                @event = new
                {
                    id = eventInfo.VaccinationEventId,
                    name = eventInfo.VaccinationEventName,
                    date = eventInfo.EventDate,
                    location = eventInfo.Location,
                    description = eventInfo.Description,
                },
                parent = new
                {
                    name = parent.FullName,
                    id = parent.ParentId,
                    students = studentList
                },
            }, member => member.Name);

            return result;
        }

        private readonly string testTemplate = "<html><head><style>.details-link { margin-top: 15px; padding: 12px; background-color: #e9ecef; border: 1px solid #dee2e6; border-radius: 5px; text-align: center; } .details-link a { font-weight: bold; color: #0056b3; text-decoration: none; font-size: 15px; } .details-link a:hover { text-decoration: underline; } .custom-message { margin-top: 15px; font-style: italic; color: #555; }</style></head><body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'><div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; background-color: #fff;'><h2 style='color: #2c3e50; text-align: center;'>Thông báo về sự kiện tiêm chủng</h2><div style='background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin: 20px 0;'><h3 style='color: #e74c3c; margin-top: 0;'>Sự kiện: {EventName}</h3><p><strong>Ngày diễn ra:</strong> {EventDate}</p><p><strong>Địa điểm:</strong> {Location}</p><p><strong>Mô tả:</strong> {Description}</p><h4 style='color: #17a2b8; margin-top: 20px;'>Áp dụng cho các học sinh:</h4><ul>{StudentList}</ul><p class='custom-message'>{CustomMessage}</p><div class='details-link'><a href='{secureDownloadUrl}' target='_blank' style='font-weight: bold; color: #0056b3; text-decoration: none;'>Tải về kế hoạch chi tiết của sự kiện</a></div></div><div style='background-color: #e8f5e8; padding: 15px; border-radius: 8px; margin: 20px 0;'><h4 style='color: #27ae60; margin-top: 0;'>Hướng dẫn phản hồi:</h4><p>Kính mong Quý Phụ huynh vui lòng nhấp vào liên kết bên dưới để truy cập biểu mẫu và xác nhận việc tham gia cho con của mình:</p><p style='text-align: center;'><a href='{ResponseLink}' style='background-color: #3498db; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block; font-weight: bold;'>Đi đến trang phản hồi</a></p></div><div style='background-color: #fff3cd; padding: 15px; border-radius: 8px; margin: 20px 0;'><h4 style='color: #856404; margin-top: 0;'>Lưu ý quan trọng:</h4><ul><li>Nếu không thể tham gia, xin vui lòng nêu rõ lý do trong biểu mẫu phản hồi.</li><li>Thông tin này sẽ được sử dụng để nhà trường lập kế hoạch tiêm chủng chu đáo.</li><li>Mọi thắc mắc vui lòng liên hệ với bộ phận y tế của nhà trường.</li></ul></div><div style='text-align: center; margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd;'><p style='color: #7f8c8d; font-size: 14px;'>Email này được gửi tự động từ hệ thống quản lý y tế học đường.<br>Vui lòng không trả lời email này.</p></div></div></body></html>";
    }
}