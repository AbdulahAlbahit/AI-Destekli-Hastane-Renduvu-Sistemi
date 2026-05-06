using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Entities;
using Data_Accese_Layer.IRepos;
namespace Data_Accese_Layer.Repos
{
    public class UserRepo:IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context=context;
        }


        public async Task<Users> AddUser(Users user)
        {
            if (user != null)
            {
                var User = user;
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                return User;
            }


            return null;
        }





    }
}
