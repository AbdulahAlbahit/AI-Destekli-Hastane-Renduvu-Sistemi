using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
using Data_Accese_Layer.IRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Repos
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context=context;
        }
        public async Task<List<ClinicDetailDto>> GetAllClinicsAsync()
        {
            var list = await _context.Clinics.Select(c => new ClinicDetailDto
            {
                ClinicId = c.ClinicId,
                ClinicNumber = c.ClinicNumber,
                DepName = c.Dept.DeptName

            }).OrderByDescending(a=>a.DepName).ToListAsync();


            return list;
                }

        public async Task<List<ClinicDetailDto>> GetClinicsByDepId(int DepId)
        {
            var list= await _context.Clinics.Where(a =>a.DeptId==DepId ).Select(c => new ClinicDetailDto
            {
                ClinicId = c.ClinicId,
                ClinicNumber = c.ClinicNumber,
                DepName = c.Dept.DeptName

            }).OrderBy(a=>a.ClinicNumber).ToListAsync();

            return list;
        }

        public async Task<ClinicDetailDto> GetClinicByIdAsync(int id)
        {
            var list = await _context.Clinics.Select(c => new ClinicDetailDto
            {
                ClinicId = c.ClinicId,
                ClinicNumber = c.ClinicNumber,
                DepName = c.Dept.DeptName

            }).Where(a => a.ClinicId == id).FirstOrDefaultAsync();

            return list;
        }

       
    }
}
