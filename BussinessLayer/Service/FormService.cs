using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.Form;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public FormService(IFormRepository formRepository, IMapper mapper, IEmailService emailService
            , IParentRepository parentRepository)
        {
            _formRepository = formRepository;
            _mapper = mapper;
            _emailService = emailService;
            _parentRepository = parentRepository;
        }

        public async Task<List<FormDTO>> GetAllFormsAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return _mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<FormDTO> GetFormByIdAsync(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            return _mapper.Map<FormDTO>(form);
        }

        public async Task<FormDTO> CreateFormAsync(CreateFormDTO formDto, string createdBy)
        {
            var form = _mapper.Map<Form>(formDto);
            form.Createddate = DateTime.UtcNow;
            form.Modifieddate = DateTime.UtcNow;
            form.Createdby = createdBy;
            form.Modifiedby = createdBy;
            form.Isaccepted = false;

            await _formRepository.AddAsync(form);
            await _formRepository.SaveChangesAsync();
            return _mapper.Map<FormDTO>(form);
        }

        public async Task<FormDTO> UpdateFormAsync(UpdateFormDTO formDto, string modifiedBy)
        {
            var form = await _formRepository.GetByIdAsync(formDto.FormId);
            if (form == null)
            {
                throw new KeyNotFoundException("Form not found");
            }
            form.Reason = formDto.Reason;
            //form.File = formDto.File;
            form.Parentid = formDto.ParentId;
            form.Modifieddate = DateTime.UtcNow;
            form.Modifiedby = modifiedBy;

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();
            return _mapper.Map<FormDTO>(form);
        }

        public async Task<bool> DeleteFormAsync(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
            {
                return false;
            }
            form.IsDeleted = true;
            form.Modifieddate = DateTime.UtcNow;
            

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<FormDTO>> GetFormsByParentIdAsync(int parentId)
        {
            var forms = await _formRepository.GetFormsByParentIdAsync(parentId);
            return _mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<List<FormDTO>> GetFormsByCategoryIdAsync(int categoryId)
        {
            var forms = await _formRepository.GetFormsByCategoryIdAsync(categoryId);
            return _mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<bool> AcceptFormAsync(ResponseFormDTO dto)
        {
            var form = _formRepository.GetAllAsync().Result.FirstOrDefault(f => f.FormId ==dto.FormId);
            if (form == null)
            {
                return false;
            }
            form.IsPending = false;

            form.Isaccepted = true;
            form.Modifieddate = DateTime.UtcNow;
            form.Reasonfordecline = dto.Reasonfordecline;
            form.Modifiedby = dto.Modifiedby;
            form.Staffid = dto.Staffid;

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();

            // Send email notification
            var emailTemplate = await _emailService.GetEmailByName("EmailTemplateKeys.FormResponseEmail");
            if (emailTemplate == null)
                return false;

            emailTemplate.To = form.Parent.Email;
            string replacedBody = FillResponseData(emailTemplate, form);
            emailTemplate.Body = replacedBody;

            await _emailService.SendEmailAsync(emailTemplate);

            return true;
        }

        public async Task<bool> DeclineFormAsync(ResponseFormDTO dto)
        {
            var form = _formRepository.GetAllAsync().Result.FirstOrDefault(f => f.FormId == dto.FormId);
            if (form == null)
            {
                return false;
            }
            form.IsPending = false;
            form.Isaccepted = false;
            form.Reasonfordecline = dto.Reasonfordecline;
            form.Modifieddate = DateTime.UtcNow;
            form.Modifiedby = dto.Modifiedby;
            form.Staffid = dto.Staffid;

            // Send email notification
            var emailTemplate = await _emailService.GetEmailByName(EmailTemplateKeys.FormResponseEmail);
            if (emailTemplate == null)
                return false;

            emailTemplate.To = form.Parent.Email;
            string replacedBody = FillResponseData(emailTemplate, form);
            emailTemplate.Body = replacedBody;

            await _emailService.SendEmailAsync(emailTemplate);

            _formRepository.Update(form);
            await _formRepository.SaveChangesAsync();
            return true;
        }

        private string FillResponseData(EmailDTO emailDTO, Form form)
        {
            var scribanTemplate = Template.Parse(emailDTO.Body);
            string result = scribanTemplate.Render(new
            {
                parent_name = form.Parent.Fullname,
                isAccepted = form.Isaccepted,
                description = form.Reasonfordecline,
                response_date = DateTime.UtcNow.ToString("dd/MM/yyyy")
            }, member => member.Name);
            return result;
        }


        //Cap thuoc
        public async Task<Form> AddFormMedicineRequest(object form, string? storedFileName, string? accessToken) {
            var addForm = _mapper.Map<Form>(form);
            addForm.Storedpath = accessToken;
            addForm.Originalfilename = storedFileName;
            await _formRepository.AddAsync(addForm);
            await _formRepository.SaveChangesAsync();
            return addForm;
        }
        //Nghi phep


    }
}
