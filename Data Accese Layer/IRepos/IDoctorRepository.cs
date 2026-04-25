using Data_Accese_Layer.Dto;
namespace Data_Accese_Layer.IRepos
{
    public interface IDoctorRepository
    {

        Task<List<DoctorDetailDto>> GetAllDoctorsAsyncc();
        Task<DoctorDetailDto> GetDoctorByIdAsync(int id);
       



    }
}