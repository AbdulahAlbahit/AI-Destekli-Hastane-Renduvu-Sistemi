using Data_Accese_Layer.Dto;
using Data_Accese_Layer.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Repos
{
    public class DoctorRepository :IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context=context;
        }




        public async Task<List<DoctorDetailDto>> GetAllDoctorsAsyncc()
        {
            return await _context.Doctors.
             Select(a => new DoctorDetailDto
             {
                 DoctorId=a.DoctorId,
                 DoctorName=a.DoctorName,
                 Specialization=a.Specialization,  
                 ClinicNumber=a.Clinic.ClinicNumber,
                 DepName=a.Clinic.Dept.DeptName,
                 Email=a.Email,
                 Phone=a.Phone
                 
             })
                .ToListAsync();

        }

        public async Task<DoctorDetailDto> GetDoctorByIdAsync(int id)
        {
            var existingItem = await _context.Doctors.Select(a => new DoctorDetailDto
            {
                DoctorId = a.DoctorId,
                DoctorName = a.DoctorName,
                Specialization = a.Specialization,
                ClinicNumber = a.Clinic.ClinicNumber,
                DepName = a.Clinic.Dept.DeptName,
                Email = a.Email,
                Phone = a.Phone

            }).SingleOrDefaultAsync(a => a.DoctorId == id);

            if (existingItem != null)
                return existingItem;

            return null;
            
        }
    }
}
