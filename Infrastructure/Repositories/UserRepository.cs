using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {

        private readonly MovieShopDbContext _movieshopDbContext;

        public UserRepository(MovieShopDbContext movieshopDbContext)
        {
            _movieshopDbContext = movieshopDbContext;
        }

        public async Task<User> AddUser(User user)
        {
            _movieshopDbContext.Users.Add(user);//add to memory
            await _movieshopDbContext.SaveChangesAsync();//only save to the database in this case!!!
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _movieshopDbContext.Users.FirstOrDefaultAsync(u=>u.Email == email);
            return user;
        }
    }
}
