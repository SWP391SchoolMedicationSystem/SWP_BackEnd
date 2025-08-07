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
    public class FormService(IFormRepository formRepository, IMapper mapper, IEmailService emailService
            , IParentRepository parentRepository, IEmailRepo emailRepo) : IFormService
    {
        public async Task<List<FormDTO>> GetAllFormsAsync()
        {
            var forms = await formRepository.GetAllAsync();
            return mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<FormDTO> GetFormByIdAsync(int id)
        {
            var form = await formRepository.GetByIdAsync(id);
            return mapper.Map<FormDTO>(form);
        }

        public async Task<FormDTO> CreateFormAsync(CreateFormDTO formDto, string createdBy)
        {
            var form = mapper.Map<Form>(formDto);
            form.Createddate = DateTime.Now;
            form.Modifieddate = DateTime.Now;
            form.Createdby = createdBy;
            form.Modifiedby = createdBy;
            form.Isaccepted = false;
            form.Ispending = true;
            await formRepository.AddAsync(form);
            await formRepository.SaveChangesAsync();
            return mapper.Map<FormDTO>(form);
        }

        public async Task<FormDTO> UpdateFormAsync(UpdateFormDTO formDto, string modifiedBy)
        {
            var form = await formRepository.GetByIdAsync(formDto.FormId);
            if (form == null)
            {
                throw new KeyNotFoundException("Form not found");
            }
            form.Reason = formDto.Reason;
            //form.File = formDto.File;
            form.Parentid = formDto.ParentId;
            form.Modifieddate = DateTime.UtcNow;
            form.Modifiedby = modifiedBy;

            formRepository.Update(form);
            await formRepository.SaveChangesAsync();
            return mapper.Map<FormDTO>(form);
        }

        public async Task<bool> DeleteFormAsync(int id)
        {
            var form = await formRepository.GetByIdAsync(id);
            if (form == null)
            {
                return false;
            }
            form.IsDeleted = true;
            form.Modifieddate = DateTime.UtcNow;
            

            formRepository.Update(form);
            await formRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<FormDTO>> GetFormsByParentIdAsync(int parentId)
        {
            var forms = await formRepository.GetFormsByParentIdAsync(parentId);
            return mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<List<FormDTO>> GetFormsByCategoryIdAsync(int categoryId)
        {
            var forms = await formRepository.GetFormsByCategoryIdAsync(categoryId);
            return mapper.Map<List<FormDTO>>(forms);
        }

        public async Task<bool> AcceptFormAsync(ResponseFormDTO dto)
        {
            var form = formRepository.GetAllAsync().Result.FirstOrDefault(f => f.FormId ==dto.FormId);
            if (form == null)
            {
                return false;
            }
            form.Ispending = false;

            form.Isaccepted = true;
            form.Modifieddate = DateTime.UtcNow;
            form.Reasonfordecline = dto.Reasonfordecline;
            form.Modifiedby = dto.Modifiedby;
            form.Staffid = dto.Staffid;


            // Send email notification
            var parent = await parentRepository.GetByIdAsync(form.Parentid.Value);
            // Send email notification
            var emailTemplate = await emailService.GetEmailByName(EmailTemplateKeys.FormResponseEmail);
            if (emailTemplate == null)
                return false;

            emailTemplate.To = form.Parent.Email;
            emailTemplate.Body = emailTemplate.Body
                .Replace("{ParentName}", parent.Fullname)
                .Replace("{Title}", form.Title)
                .Replace("{Reason}", form.Reason)
                .Replace("{Reasonfordecline}", form.Reasonfordecline)
                .Replace("{Modifieddate}", form.Modifieddate.ToString())
                .Replace("{accepted}", "<div><p>Yêu cầu của Quý phụ huynh đã được <span class=\"status-accepted\">CHẤP THUẬN</span>.</p><p>Chúng tôi sẽ tiến hành các bước tiếp theo dựa trên nội dung yêu cầu. Chúng tôi sẽ liên hệ lại nếu cần thêm thông tin.</p></div>");
            

            await emailService.SendEmailAsync(emailTemplate);
            formRepository.Update(form);
            await formRepository.SaveChangesAsync();


            return true;
        }

        public async Task<bool> DeclineFormAsync(ResponseFormDTO dto)
        {
            var form = formRepository.GetAllAsync().Result.FirstOrDefault(f => f.FormId == dto.FormId);
            if (form == null)
            {
                return false;
            }
            form.Ispending = false;
            form.Isaccepted = false;
            form.Reasonfordecline = dto.Reasonfordecline;
            form.Modifieddate = DateTime.UtcNow;
            form.Modifiedby = dto.Modifiedby;
            form.Staffid = dto.Staffid;
            var parent = await parentRepository.GetByIdAsync(form.Parentid.Value);
            // Send email notification
            var emailTemplate = await emailService.GetEmailByName(EmailTemplateKeys.FormResponseEmail);
            if (emailTemplate == null)
                return false;

            emailTemplate.To = form.Parent.Email;
            emailTemplate.Body = emailTemplate.Body
                .Replace("{ParentName}",parent.Fullname)
                .Replace("{Title}", form.Title)
                .Replace("{Reason}",form.Reason)
                .Replace("{accepted}", "<div><p>Rất tiếc, yêu cầu của Quý phụ huynh đã bị <span class=\"status-declined\">TỪ CHỐI</span>.</p><div class=\"reason-decline\"><p><strong>Lý do từ chối:</strong> {Reasonfordecline}</p><p><strong>Ngày xử lý:</strong> {Modifieddate}</p></div><p>Nếu có bất kỳ thắc mắc nào, xin vui lòng liên hệ lại với chúng tôi. Cảm ơn Quý phụ huynh đã quan tâm.</p></div>")
                                .Replace("{Reasonfordecline}", form.Reasonfordecline)
                .Replace("{Modifieddate}", form.Modifieddate.ToString())
;

            await emailService.SendEmailAsync(emailTemplate);

            formRepository.Update(form);
            await formRepository.SaveChangesAsync();
            return true;
        }



        //Cap thuoc
        public async Task<Form> AddFormMedicineRequest(object form, string? storedFileName, string? accessToken) {
            var addForm = mapper.Map<Form>(form);
            addForm.Createddate = DateTime.Now; 
            addForm.Storedpath = accessToken;
            addForm.Originalfilename = storedFileName;
            addForm.Ispending = true;
            addForm.Isaccepted = false;
            await formRepository.AddAsync(addForm);
            await formRepository.SaveChangesAsync();
            return addForm;
        }
        //Nghi phep


    }
}
