﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Consultations;
using DataAccessLayer.Entity;

namespace BussinessLayer.IService
{
    public interface IConsultationTypeService
    {
        Task<List<Consultationtype>> GetAllConsultationTypesAsync();

        Task<Consultationtype> GetConsultationTypeByIdAsync(int id);
        Task<Consultationtype> AddConsultationTypeAsync(CreateConsultationTypeDTO consultationType);
        Task<Consultationtype> UpdateConsultationTypeAsync(Consultationtype consultationType);
        void DeleteConsultationType(int id);
    }
}
