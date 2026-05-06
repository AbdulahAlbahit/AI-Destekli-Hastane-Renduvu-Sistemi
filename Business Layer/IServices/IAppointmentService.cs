using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;

namespace Business_Layer.IServices
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDetailDto>> GetAllAppointment();
        Task<AppointmentDetailDto> GetAppointmentById(int id);
        Task<bool> AddAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(Appointment appointment,int id);
        Task<bool> DeleteAppointment(int id);
        public Task<bool> isThisDateavailable(DateOnly time, TimeOnly saat);
        Task<AppointmentDetailDto> GetAppointmentByUserId(int userId);
    }
}