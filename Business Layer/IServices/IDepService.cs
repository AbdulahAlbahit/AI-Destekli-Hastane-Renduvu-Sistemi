using Data_Accese_Layer.Dto;

namespace Business_Layer.IServices
{
    public interface IDepService
    {
        Task<List<DeptDetailDto>> GetAllDepartmentsAsync();
    }
}