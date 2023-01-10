using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public UserRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public UserTableEntity UserRegistration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserTableEntity User = new UserTableEntity();
                User.FirstName = userRegistrationModel.FirstName;
                User.Lastname = userRegistrationModel.Lastname;
                User.Email = userRegistrationModel.Email;
                User.Password = EncryptPassword(userRegistrationModel.Password);
                User.UserId = new UserTableEntity().UserId;

                fundooContext.UserTable.Add(User);
                fundooContext.SaveChanges();
                return User;
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
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == userLoginModel.Email);
                if (result != null)
                {
                    //ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    //IDatabase database = connectionMultiplexer.GetDatabase();
                    //database.StringSet(key:"FirstName",result.FirstName);
                    //database.StringSet(key:"LastName",result.Lastname);
                    //database.StringSet(key:"UserId",result.UserId.ToString());
                    string decryPass = DecryptPassword(result.Password);
                    if (decryPass == userLoginModel.Password)
                    {
                        var token = this.GenerateToken(result.Email, result.UserId);
                        return token;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateToken(string Email, long UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration[("Jwt:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("UserId", UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string EncryptPassword(string Password)
        {
            try
            {
                byte[] enData_byte = new byte[Password.Length];
                enData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(enData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptPassword(string Password)
        {
            System.Text.UTF8Encoding encoder = new UTF8Encoding();
            System.Text.Decoder decoder = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Password);
            int charCount = decoder.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decodedchar = new char[charCount];
            decoder.GetChars(todecode_byte, 0, todecode_byte.Length, decodedchar, 0);
            string result = new string(decodedchar);
            return result;
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    MSMQ ms = new MSMQ();
                    ms.sendData2Queue(token,result.Email,result.FirstName);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
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
                if (Password.Equals(ConfirmPassword))
                {
                    var user = this.fundooContext.UserTable.Where(x => x.Email == Email).FirstOrDefault();
                    string newEncryptedPassword = EncryptPassword(Password);
                    user.Password = newEncryptedPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
