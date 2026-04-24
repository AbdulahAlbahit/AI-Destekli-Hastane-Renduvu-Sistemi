using Data_Accese_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Repos
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointment();   
        Task<Appointment> GetAppointmentById(int id);
        Task AddAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task DeleteAppointment(int id);

    }
}
