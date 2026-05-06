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
    }
}
