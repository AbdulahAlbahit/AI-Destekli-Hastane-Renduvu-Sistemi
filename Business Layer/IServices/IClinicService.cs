using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.IServices
{
     public interface IClinicService
    {

        Task<List<ClinicDetailDto>> GetAllClinicsAsync();
        Task<ClinicDetailDto> GetClinicByIdAsync(int id);
        Task<List<ClinicDetailDto>> GetClinicByDepId(int DepId);
      
    }
}
