using Data_Accese_Layer.Entities;

namespace Business_Layer.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointment();
        Task<Appointment> GetAppointmentById(int id);
        Task AddAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task DeleteAppointment(int id);
    }
}