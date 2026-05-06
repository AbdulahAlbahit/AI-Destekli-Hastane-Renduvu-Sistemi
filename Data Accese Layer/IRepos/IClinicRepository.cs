using Data_Accese_Layer.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.IRepos
{
    public interface IClinicRepository
    {
        Task<List<ClinicDetailDto>> GetAllClinicsAsync();
        Task<ClinicDetailDto> GetClinicByIdAsync(int id);
        Task<List<ClinicDetailDto>> GetClinicsByDepId(int DepId);

    }
}
