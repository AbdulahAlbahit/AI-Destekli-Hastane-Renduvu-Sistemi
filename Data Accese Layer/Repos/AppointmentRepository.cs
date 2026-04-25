using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Repos
{
    public  class AppointmentRepository:IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
                _context   = context;
        }
        public async Task<List<AppointmentDetailDto>> GetAllAppointment()
        {
            return await _context.Appointments.Select(a => new AppointmentDetailDto
            {
             AppointmentId=a.AppointmentId,
              ClinicNum=a.Clinic.ClinicNumber,
             DepName=a.Clinic.Dept.DeptName,
             DoctorName=a.Doctor.DoctorName,
             PatientName=a.Patient.PatientName,
             PatientGender=a.Patient.Gender,
             PatientPhone=a.Patient.Phone,
             TheDate=a.TheDate,
             TheStatus=a.TheStatus,
             TheTime=a.TheTime

            }).ToListAsync();
              
                ;
           
            
            
            
        }

        public async Task<AppointmentDetailDto> GetAppointmentById(int id)
        {
            var appoint = await _context.Appointments.Select(a=>new AppointmentDetailDto
            {
                AppointmentId = a.AppointmentId,
                TheTime=a.TheTime,
                TheStatus = a.TheStatus,
                TheDate = a.TheDate,
                DoctorName=a.Doctor.DoctorName,
                ClinicNum=a.Clinic.ClinicNumber,
                DepName=a.Clinic.Dept.DeptName,
                PatientName=a.Patient.PatientName,
                PatientGender=a.Patient.Gender,
                PatientPhone= a.Patient.Phone


            }).Where(a=>a.AppointmentId==id).FirstOrDefaultAsync();




            if (appoint == null)
                return null;

            return appoint;
        }

        public async Task<bool> AddAppointment(Appointment  appointment)
        {
          
            if (appointment != null) { 
            _context.Appointments.Add(appointment);
                  await   _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAppointment(int id)
        {      
        var  appointment = await _context.Appointments.SingleOrDefaultAsync(c=>c.AppointmentId == id);
            if (appointment != null) { 
            _context.Appointments.Remove(appointment);
               await  _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

     
        public async Task<bool>  UpdateAppointment(Appointment appointment, int id  )
        {
            var app=_context.Appointments.SingleOrDefault(c=>c.AppointmentId == id);
            if (app != null)
            {
               app.TheTime=appointment.TheTime;
               app.TheDate=appointment.TheDate;
               app.TheStatus=appointment.TheStatus;
               app.ClinicId=appointment.ClinicId;
               app.DoctorId=appointment.DoctorId;
               app.PatientId=appointment.PatientId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
