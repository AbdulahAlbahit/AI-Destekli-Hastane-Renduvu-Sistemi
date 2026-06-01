using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Entities;
using Data_Accese_Layer.IRepos;
namespace Business_Layer.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo=repo;
        }


        public async Task<Users> AddUser(Users user)
        {
            return await _repo.AddUser(user);
        }

        public async Task<Users> CheckUser(Users user)
        {
            return await _repo.CheckUser(user);
        }
    }
}
