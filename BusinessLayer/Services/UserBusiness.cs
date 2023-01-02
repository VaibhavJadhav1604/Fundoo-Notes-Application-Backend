using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public UserTableEntity UserRegistration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return this.userRepo.UserRegistration(userRegistrationModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                return this.userRepo.UserLogin(userLoginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                return this.userRepo.ForgotPassword(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(string Email, string Password, string ConfirmPassword)
        {
            try
            {
                return this.userRepo.ResetPassword(Email, Password, ConfirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
