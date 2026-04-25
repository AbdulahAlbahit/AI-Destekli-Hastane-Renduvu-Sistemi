using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.IRepos
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentDetailDto>> GetAllAppointment();   
        Task<AppointmentDetailDto> GetAppointmentById(int id);
        Task<bool> AddAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(Appointment appointment,int id);
        Task<bool> DeleteAppointment(int id);

    }
}
