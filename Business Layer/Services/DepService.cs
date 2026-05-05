using Data_Accese_Layer;
using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Dto;
using Microsoft.EntityFrameworkCore;
using Data_Accese_Layer.IRepos;
namespace Business_Layer.Services
{
    internal class DepService
    {
        public DepService() { }
       
        private readonly IDepRepo _deprepo;

        public DepService(IDepRepo depRepo)
        {
            _deprepo=depRepo;
        }

        public async Task<List<DeptDetailDto>> GetAllDepartmentsAsync()
        {
            // اكتب الكود هنا كما فعلنا سابقاً
            return await _deprepo.GetAllDepartmentsAsync();
                //.Select(d => new DeptDetailDto
                //{
                //    DeptId = d.DeptId,
                //    DeptName = d.DeptName
                //}).ToListAsync();
        }
    }
}
