using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Data_Accese_Layer.Dto
{
    public class DepRepo : IDepRepo
    {
        private readonly AppDbContext _context;

        public DepRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeptDetailDto>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Select(d => new DeptDetailDto
                {
                    DeptId = d.DeptId,
                    DeptName = d.DeptName
                })
                .ToListAsync();

            return departments;
        }

        public async Task<DeptDetailDto> GetDepByName(string name)
        {
          var dep= await _context.Departments.Select(a => new DeptDetailDto
            {
                DeptName = a.DeptName,
                DeptId = a.DeptId
            }
                ).FirstOrDefaultAsync(a => a.DeptName == name);

            return dep;

        }
    }
}
