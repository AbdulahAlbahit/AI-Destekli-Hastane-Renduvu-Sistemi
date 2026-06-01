using Data_Accese_Layer.Dto;

namespace Business_Layer.IServices
{
    public interface IDoctorServices
    {
        Task<List<DoctorDetailDto>> GetAllDoctorsAsyncc();
        Task<DoctorDetailDto> GetDoctorByIdAsync(int id);
        Task<List<DoctorDetailDto>> GetDoctorbyClinicIdAsync(int ClinicId);





    }
}