using Data_Accese_Layer.Entities;
using Data_Accese_Layer.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointment;

        public AppointmentService(IAppointmentRepository appointment)
        {
            _appointment = appointment;
        }

        public async Task AddAppointment(Appointment appointment)
        {
           await _appointment.AddAppointment(appointment);
        }

        public async Task DeleteAppointment(int id)
        {
          await  _appointment.DeleteAppointment(id); 
        }

        public async Task<List<Appointment>> GetAllAppointment()
        {
            return await _appointment.GetAllAppointment();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
          return await _appointment.GetAppointmentById(id);
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            await _appointment.UpdateAppointment(appointment);
        }
    }
}
