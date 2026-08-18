using Business_Layer.Dto;
using Data_Accese_Layer.Entities;

namespace Business_Layer.IServices
{
    public interface IPatientService
    {
        Task<bool> AddPatient(Patient patient);
        Task<Patient> GetPatient(int UserId);
        Task<bool> UpdatePatient(int userId, PatientUpdateDto dto);
    }
}