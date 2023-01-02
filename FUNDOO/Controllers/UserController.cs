using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using StackExchange.Redis;
using System.Security.Claims;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger)
        {
            this.userBusiness = userBusiness;
            this.logger = logger;
        }
        [HttpPost("UserRegister")]
        public ActionResult UserRegistration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var response = this.userBusiness.UserRegistration(userRegistrationModel);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = "SuccessFull", data = response });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "UnSuccessFull", data = response }); ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("UserLogin")]
        public ActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var response = this.userBusiness.UserLogin(userLoginModel);
                //if (response != null)
                //{
                //    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                //    IDatabase database = connectionMultiplexer.GetDatabase();
                //    string FirstName=database.StringGet("FirstName");
                //    string LastName = database.StringGet("LastName");
                //    long UserId = Convert.ToInt32(database.StringGet("UserId"));
                //    this.logger.LogInformation(FirstName + " Is LoggerIn");
                //    UserTableEntity  userTable=new UserTableEntity
                //    {
                //        UserId = UserId,
                //        FirstName = FirstName,
                //        Lastname = LastName,
                //        Email = userLoginModel.Email
                //    };
                //    logger.LogInformation("SuccessFully");
                //    return this.Ok(new { success = true, message = "Successfull",token=response });
                //}
                //else
                //{
                //    logger.LogWarning("UnSuccessFully");
                //    return this.BadRequest(new { success = false, message = "UnSuccessfull" });
                //}
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Login successfull", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                var response = this.userBusiness.ForgotPassword(Email);
                if (response != null)
                {
                    return this.Ok(new { success = true, data = "Mail Sent",result=response});
                }
                else
                {
                    return this.BadRequest(new { success = false, data = "Mail Not Sent" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string Password, string ConfirmPassword)
        {
            try
            {
                string Email= User.FindFirst(ClaimTypes.Email).Value.ToString();
                var response = this.userBusiness.ResetPassword(Email, Password, ConfirmPassword);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Password Changed Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Password Changing Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
