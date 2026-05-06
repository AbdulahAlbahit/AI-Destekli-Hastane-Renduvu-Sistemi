using Data_Accese_Layer.Entities;

namespace Data_Accese_Layer.IRepos
{
    public interface IUserRepo
    {
        Task<Users> AddUser(Users user);
    }
}