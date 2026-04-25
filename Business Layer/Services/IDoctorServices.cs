using Data_Accese_Layer.Dto;

namespace Business_Layer.Services
{
    public interface IDoctorServices
    {
        Task<List<DoctorDetailDto>> GetAllDoctorsAsyncc();
        Task<DoctorDetailDto> GetDoctorByIdAsync(int id);






    }
}