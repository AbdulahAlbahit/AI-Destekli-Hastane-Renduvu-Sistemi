using Data_Accese_Layer.Dto;
using Data_Accese_Layer.IRepos;
using Data_Accese_Layer.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class DoctorServices:IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository=doctorRepository;
        }

        public async Task<List<DoctorDetailDto>> GetAllDoctorsAsyncc()
        {
           return await _doctorRepository.GetAllDoctorsAsyncc();
        }

        public async Task<DoctorDetailDto> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }



    }
}
