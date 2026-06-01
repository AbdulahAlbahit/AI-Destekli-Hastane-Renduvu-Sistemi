using Business_Layer.IServices;
using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
using Data_Accese_Layer.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointment;
        private  const int sinir=10;
        public AppointmentService(IAppointmentRepository appointment)
        {
            _appointment = appointment;
        }

        public async Task<bool> AddAppointment(Appointment appointment)
        {
            if (await _appointment.AddAppointment(appointment))
                return true;

           return false;

        }

        public async Task<bool> DeleteAppointment(int id)
        {
            if (await _appointment.DeleteAppointment(id))
                return true;
            return false;
                
        }

        public async Task<List<AppointmentDetailDto>> GetAllAppointment()
        {
            return await _appointment.GetAllAppointment();
        }

        public async Task<AppointmentDetailDto> GetAppointmentById(int id)
        {
          return await _appointment.GetAppointmentById(id);
        }

        public async Task<AppointmentDetailDto> GetAppointmentByUserId(int userId)
        {
            return await _appointment.GetAppointmentByUserId(userId);

        }
        public async Task<bool> UpdateAppointment(Appointment appointment,int id)
        {
            
            if (await _appointment.UpdateAppointment(appointment,id))
                return true;

            return false;

                }



        public async Task<bool> isThisDateavailable(DateOnly time,TimeOnly saat)
        {
            var dates = await _appointment.GetTheDates(time);

            foreach (var item in dates)
            {
                TimeOnly start = new TimeOnly(saat.Hour, saat.Minute);
                TimeOnly end = new TimeOnly(item.TheTime.Hour, item.TheTime.Minute);
                TimeSpan fark = end - start;
                if (Math.Abs(fark.TotalMinutes) < sinir)
                    return false;
            }
            return true;
        }

       
    }
}
