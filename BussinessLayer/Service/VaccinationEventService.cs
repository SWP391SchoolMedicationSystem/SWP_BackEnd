using System;
using System.Reflection;
using AutoMapper;
using BussinessLayer.IService;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public VaccinationEventService(
            IVaccinationEventRepository vaccinationEventRepository,
            IVaccinationRecordRepository vaccinationRecordRepository,
            IEmailService emailService,
            IEmailRepo emailRepo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IParentService parentService)
        {
            _vaccinationEventRepository = vaccinationEventRepository;
            _vaccinationRecordRepository = vaccinationRecordRepository;
            _emailService = emailService;
            _emailRepo = emailRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _parentService = parentService;
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

        public async Task<VaccinationEventDTO> CreateEventAsync(CreateVaccinationEventDTO dto, string createdBy)
        {
            var vaccinationEvent = _mapper.Map<Vaccinationevent>(dto);
            vaccinationEvent.Createddate = DateTime.Now;
            vaccinationEvent.Modifieddate = DateTime.Now;
            vaccinationEvent.Createdby = createdBy;
            vaccinationEvent.Modifiedby = createdBy;
            vaccinationEvent.Isdeleted = false;

            await _vaccinationEventRepository.AddAsync(vaccinationEvent);
            await _vaccinationEventRepository.SaveChangesAsync();

            return _mapper.Map<VaccinationEventDTO>(vaccinationEvent);
        }

        public async Task<VaccinationEventDTO> UpdateEventAsync(UpdateVaccinationEventDTO dto, string modifiedBy)
        {
            var existingEvent = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
            if (existingEvent == null)
                throw new InvalidOperationException("Vaccination event not found.");

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

        public async Task<List<EmailDTO>> SendVaccinationEmailToAllParentsAsync(SendVaccinationEmailDTO dto)
        {
            try
            {
                var eventInfo = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
                if (eventInfo == null)
                    return null;

                var emailTemplate = await _emailRepo.GetEmailTemplateByIdAsync(dto.EmailTemplateId);
                if (emailTemplate == null)
                    return null;

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
                            .Replace("{ResponseLink}", GenerateResponseLink(parent.Email!, dto.VaccinationEventId))
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

        public async Task<List<EmailDTO>> SendVaccinationEmailToSpecificParentsAsync(SendVaccinationEmailDTO dto, List<int> parentIds)
        {
            try
            {
                var eventInfo = await _vaccinationEventRepository.GetByIdAsync(dto.VaccinationEventId);
                if (eventInfo == null)
                    return null;

                var emailTemplate = await _emailRepo.GetEmailTemplateByIdAsync(dto.EmailTemplateId);
                if (emailTemplate == null)
                    return null;

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
                            .Replace("{ResponseLink}", GenerateResponseLink(parent.Email!, dto.VaccinationEventId))
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

        private string GenerateResponseLink(string email, int eventId)
        {
            // This would typically generate a secure link to a web form
            // For now, we'll create a simple link structure
            var baseUrl = _httpContextAccessor.HttpContext?.Request.Scheme + "://" +
                         _httpContextAccessor.HttpContext?.Request.Host;

            return $"{baseUrl}/api/VaccinationEvent/Respond?email={Uri.EscapeDataString(email)}&eventId={eventId}";
        }

        public async Task<string> FillEmailTemplateData(string email, VaccinationEventDTO eventInfo)
        {
            var emailTemplate = _emailService.GetTemplateByID(6);
            var parent = await _parentService.GetParentByEmailForEvent(email);
            var scribanTemplate = Template.Parse(emailTemplate.Body);
            var students = parent.Students
                .Select(s => new
                {
                    name = s.Fullname,
                    id = s.StudentId
                }).ToList();

            string result = await scribanTemplate.RenderAsync(new
            {
                @event = new
                {
                    id = eventInfo.VaccinationEventId,
                    name = eventInfo.VaccinationEventName,
                    date = eventInfo.EventDate,
                    location = eventInfo.Location,
                    description = eventInfo.Description
                },
                parent = new
                {
                    name = parent.FullName,
                    id = parent.ParentId,
                    students = students
                },
            }, member => member.Name);

            return result;
        }
    }
}