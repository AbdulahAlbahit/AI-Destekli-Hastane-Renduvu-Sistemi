using Business_Layer.Dto;
using Business_Layer.IServices;
using Data_Accese_Layer.Entities;
using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepo _repo;

        public PatientService(IPatientRepo repo)
        {
            _repo=repo;
        }
        public async  Task<bool> AddPatient(Patient patient)
        {
            return await _repo.AddPatient(patient); 
        }

        public async Task<Patient> GetPatient(int UserId)
        {
            return await _repo.GetPatient(UserId);
        }

        public async Task<bool> UpdatePatient(int userId, PatientUpdateDto dto)
        {
            var patient = await _repo.GetPatient(userId);
            bool isNew = false;
            
            if (patient == null) 
            {
                patient = new Patient { UserId = userId };
                isNew = true;
            }

            patient.PatientName = !string.IsNullOrWhiteSpace(dto.PatientName) ? dto.PatientName : (patient.PatientName ?? "Bilinmiyor");
            patient.Phone = dto.Phone;
            patient.Gender = dto.Gender ?? "Erkek";
            
            if (DateOnly.TryParse(dto.DateOfBirth, out DateOnly dob))
            {
                patient.DateOfBirth = dob;
            }
            else if (isNew)
            {
                patient.DateOfBirth = new DateOnly(2000, 1, 1); // Default for new if invalid
            }

            if (isNew)
            {
                return await _repo.AddPatient(patient);
            }
            else
            {
                return await _repo.UpdatePatient(patient);
            }
        }
    }
}
