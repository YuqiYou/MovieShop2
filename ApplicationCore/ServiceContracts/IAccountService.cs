using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceContracts
{
    public interface IAccountService
    {

        Task<bool> CreateUser(UserRegisterModel model);

        Task<bool> ValidateUser(UserLoginModel model);

    }
}
