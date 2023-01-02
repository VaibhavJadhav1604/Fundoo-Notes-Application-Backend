using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserTableEntity UserRegistration(UserRegistrationModel userRegistrationModel);
        public string UserLogin(UserLoginModel userLoginModel);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string Email, string Password, string ConfirmPassword);
    }
}
