using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class AccountService:IAccountService
    {

        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(UserRegisterModel model)
        {
            //1 check if the email exists in db
            var user = await _userRepository.GetUserByEmail(model.Email);
            if(user != null)
            {
                throw new Exception("Email already exists, please try to login");
            }


            //continue with registration
            //create a unique salt and hash the password with salt

            var salt = GetRandomSalt();
            var hashedPassword = GetHashedPasswordWithSalt(model.Password,salt);

            //save the User into User Table using User Repository
            var dbUser = new User {Email = model.Email,FirstName = model.FirstName, LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,Salt = salt, HashedPassword = hashedPassword
            };

            var savedUser = await _userRepository.AddUser(dbUser);
            if(savedUser.Id > 0)
            {
                return true;
            }
            return false;
        }

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        private string GetHashedPasswordWithSalt(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            Convert.FromBase64String(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8));
            return hashed;
        }

        public async Task<bool> ValidateUser(UserLoginModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                throw new Exception("Please register first");
            }

            var hashedPassword = GetHashedPasswordWithSalt(model.Password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                return true;
            }
            return false;

        }
    }
}
