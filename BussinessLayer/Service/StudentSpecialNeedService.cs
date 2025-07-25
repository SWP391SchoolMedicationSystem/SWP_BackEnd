﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLayer.IService;
using DataAccessLayer.DTO.StudentSpecialNeeds;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Service
{
    public class StudentSpecialNeedService : IStudentSpecialNeedService
    {
        private readonly IStudentSpecialNeedRepo _studentNeedRepository;
        private readonly IStudentRepo _studentRepository;
        private readonly IMapper _mapper;
        private readonly IStudentSpecialNeedCategoryRepo _studentSpecialNeedCategoryRepo;
        public StudentSpecialNeedService(IStudentSpecialNeedRepo studentNeedRepository, IStudentRepo studentRepo, IMapper mapper, IStudentSpecialNeedCategoryRepo studentSpecialNeedCategoryRepo)
        {
            _studentNeedRepository = studentNeedRepository;
            _studentRepository = studentRepo;
            _mapper = mapper;
            _studentSpecialNeedCategoryRepo = studentSpecialNeedCategoryRepo;
        }

        public void AddStudentSpecialNeed(CreateSpecialStudentNeedDTO studentSpecialNeed)
        {
            StudentSpecialNeed student = _mapper.Map<StudentSpecialNeed>(studentSpecialNeed);
            _studentNeedRepository.AddAsync(student);
            _studentNeedRepository.Save();
        }

        public void DeleteStudentSpecialNeed(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid student special need ID.", nameof(id));
            }
            var studentSpecialNeed = _studentNeedRepository.GetByIdAsync(id).Result;
            if (studentSpecialNeed == null)
            {
                throw new KeyNotFoundException("Student special need not found.");
            }
            studentSpecialNeed.Isdeleted = true;
            _studentNeedRepository.Update(studentSpecialNeed);
            _studentNeedRepository.Save();
        }

        public async Task<List<StudentSpecialNeedDTO>> GetAllStudentSpecialNeedsAsync()
        {
            var studentSpecialNeeds = await _studentNeedRepository.GetAllAsync();
            var dtos = _mapper.Map<List<StudentSpecialNeedDTO>>(studentSpecialNeeds);
            return dtos;
        }

        public async Task<StudentSpecialNeedDTO> GetStudentSpecialNeedByIdAsync(int id)
        {
            var studentSpecialNeed = await _studentNeedRepository.GetByIdAsync(id);
            if (studentSpecialNeed == null)
            {
                throw new KeyNotFoundException("Student special need not found.");
            }
            var dto = _mapper.Map<StudentSpecialNeedDTO>(studentSpecialNeed);
            return dto;
        }

        public async Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByCategoryIdAsync(int categoryId)
        {
            var studentSpecialNeeds = await _studentNeedRepository.GetAllAsync();
            var filteredNeeds = studentSpecialNeeds.Where(s => s.SpecialNeedCategoryId == categoryId).ToList();
            var dtos = _mapper.Map<List<StudentSpecialNeedDTO>>(filteredNeeds);
            return dtos;
        }

        public Task<List<StudentSpecialNeedDTO>> GetStudentSpecialNeedsByStudentIdAsync(int studentId)
        {
            var studentSpecialNeeds = _studentNeedRepository.GetAllAsync().Result;
            var filteredNeeds = studentSpecialNeeds.Where(s => s.StudentId == studentId).ToList();
            var dtos = _mapper.Map<List<StudentSpecialNeedDTO>>(filteredNeeds);
            return Task.FromResult(dtos);
        }

        public void UpdateStudentSpecialNeed(UpdateStudentSpecialNeedDTO studentSpecialNeed)
        {
            if (studentSpecialNeed == null)
            {
                throw new ArgumentNullException(nameof(studentSpecialNeed), "Student special need cannot be null.");
            }
            var entity = _mapper.Map<StudentSpecialNeed>(studentSpecialNeed);
            var existingEntity = _studentNeedRepository.GetByIdAsync(entity.StudentSpecialNeedId).Result;
            if (existingEntity == null)
            {
                throw new KeyNotFoundException("Student special need not found.");
            }
            existingEntity.StudentId = entity.StudentId;
            existingEntity.SpecialNeedCategoryId = entity.SpecialNeedCategoryId;
            existingEntity.Notes = entity.Notes;
            existingEntity.Isdeleted = entity.Isdeleted;
            _studentNeedRepository.Update(existingEntity);
            _studentNeedRepository.Save();
        }
    }
}
