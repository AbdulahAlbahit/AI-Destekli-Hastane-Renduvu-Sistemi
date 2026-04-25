using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;

namespace Business_Layer.Services
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDetailDto>> GetAllAppointment();
        Task<AppointmentDetailDto> GetAppointmentById(int id);
        Task<bool> AddAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(Appointment appointment,int id);
        Task<bool> DeleteAppointment(int id);
    }
}