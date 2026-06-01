using Business_Layer.IServices;
using Data_Accese_Layer.Dto;
using Data_Accese_Layer.IRepos;
using Data_Accese_Layer.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository=clinicRepository; 
        }


        public async Task<List<ClinicDetailDto>> GetAllClinicsAsync()
        {
            return await _clinicRepository.GetAllClinicsAsync();
        }

        public async Task<List<ClinicDetailDto>> GetClinicByDepId(int DepId)
        {
            return await _clinicRepository.GetClinicsByDepId(DepId) ;
        }

        public async Task<ClinicDetailDto> GetClinicByIdAsync(int id)
        {
            return await _clinicRepository.GetClinicByIdAsync(id) ;
        }
    }
}
