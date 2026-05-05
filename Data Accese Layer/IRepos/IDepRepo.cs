using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Dto;
namespace Data_Accese_Layer.IRepos
{
    public interface IDepRepo
    {
        Task<List<DeptDetailDto>> GetAllDepartmentsAsync();
    }
}
