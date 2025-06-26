using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using NPOI.OpenXmlFormats.Dml;

namespace BussinessLayer.Service
{
    public class MedicineDonationService(IMedicineDonationRepository medicineDonationRepository, IParentRepository parentRepository,
        IMedicineRepository medicineRepository, IMapper mapper) : IMedicineDonationService
    {
        public Task AddMedicineDonationAsync(MedicineDonationDto medicinedonation)
        {
            var MedicineDonationEntity = mapper.Map<Medicinedonation>(medicinedonation);
            MedicineDonationEntity.Medicine = medicineRepository.GetByIdAsync(medicinedonation.Medicineid).Result;

            MedicineDonationEntity.Parent = parentRepository.GetByIdAsync(medicinedonation.Parentid.Value).Result;
            if(MedicineDonationEntity.Parent == null)
            {
                return Task.FromException(new KeyNotFoundException("Parent not found."));
            }

            MedicineDonationEntity.Status = false;
            MedicineDonationEntity.Createddate = DateTime.Now;
            MedicineDonationEntity.Medicine = medicineRepository.GetByIdAsync(medicinedonation.Medicineid).Result;
            if (MedicineDonationEntity.Medicine == null)
            {
                return Task.FromException(new KeyNotFoundException("Medicine not found."));
            }
            medicineDonationRepository.AddAsync(MedicineDonationEntity);
            medicineDonationRepository.Save();
            return Task.CompletedTask;
        }

        public void DeleteMedicineDonation(int id)
        {
            var medicineDonation = medicineDonationRepository.GetByIdAsync(id).Result;
            if (medicineDonation != null)
            {
                medicineDonation.Isdeleted = true;
                medicineDonation.Modifieddate = DateTime.Now;
                medicineDonationRepository.Update(medicineDonation);
                medicineDonationRepository.Save();
            }
            else
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
        }

        public Task<List<Medicinedonation>> GetAllMedicineDonationsAsync()
        {
            return medicineDonationRepository.GetAllAsync();
        }

        public Task<Medicinedonation> GetMedicineDonationByIdAsync(int id)
        {
            return medicineDonationRepository.GetByIdAsync(id);
        }

        public Task<List<Medicinedonation>> GetMedicineDonationsByMedicineIdAsync(int medicineId)
        {
            var medicineDonations = medicineDonationRepository.GetAllAsync();
            return medicineDonations.ContinueWith(task =>
            {
                return task.Result.Where(md => md.Medicineid == medicineId).ToList();
            });
        }

        public Task<List<Medicinedonation>> GetMedicineDonationsByParentIdAsync(int parentId)
        {
            var medicineDonations = medicineDonationRepository.GetAllAsync();
            return medicineDonations.ContinueWith(task =>
            {
                return task.Result.Where(md => md.Parentid == parentId).ToList();
            });
        }

        public Task<List<Medicinedonation>> SearchMedicineDonationsAsync(string searchTerm)
        {
            var medicineDonations = medicineDonationRepository.GetAllAsync();
            return medicineDonations.ContinueWith(task =>
            {
                return task.Result.Where(donation =>
                    donation.Medicine.Medicinename.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (donation.Parent != null && donation.Parent.Fullname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            });
        }

        public void UpdateMedicineDonation(int DonationId,MedicineDonationDto medicinedonation)
        {
            var medicineDonationEntity = medicineDonationRepository.GetByIdAsync(DonationId).Result;
            if(medicineDonationEntity == null)
            {
                throw new KeyNotFoundException("Medicine donation not found.");
            }
            else
            {
                medicineDonationEntity.Modifieddate = DateTime.Now;
                medicineRepository.Update(medicineDonationEntity.Medicine);

            }


        }
    }
}
