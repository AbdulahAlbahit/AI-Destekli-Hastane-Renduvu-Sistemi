using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Accese_Layer.Repos
{
    public class PatientRepo:IPatientRepo
    {
        private readonly AppDbContext _context;

        public PatientRepo(AppDbContext context)
        {
            _context=context;
        }
        public async Task<bool> AddPatient(Patient patient)
        {
            if(patient != null)
            {
                 _context.Patients.Add(patient);
                await _context.SaveChangesAsync();  
                return true;
            }
            return false;


        }

        public async Task<Patient> GetPatient(int UserId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(c => c.UserId == UserId);
            return patient;


        }

    }
}
