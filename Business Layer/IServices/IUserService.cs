using Data_Accese_Layer.Entities;

namespace Business_Layer.Services
{
    public interface IUserService
    {
        Task<Users> AddUser(Users user);
        Task<Users> CheckUser(Users user);
    }
}