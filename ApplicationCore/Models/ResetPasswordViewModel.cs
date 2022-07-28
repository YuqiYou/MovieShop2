using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ResetPasswordViewModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string repeatNewPassword { get; set; }
    }
}
