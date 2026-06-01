using Data_Accese_Layer;
using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Dto;
using Microsoft.EntityFrameworkCore;
using Data_Accese_Layer.IRepos;
using Business_Layer.IServices;
namespace Business_Layer.Services
{
    public class DepService:IDepService

    {
     
        private readonly IDepRepo _deprepo;

        public DepService(IDepRepo depRepo)
        {
            _deprepo=depRepo;
        }

        public async Task<List<DeptDetailDto>> GetAllDepartmentsAsync()
        {
           
            return await _deprepo.GetAllDepartmentsAsync();
                
        }
    }
}
