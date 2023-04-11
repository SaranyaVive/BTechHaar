using BTechHaar.Data.Context;
using BTechHaar.Data.DataModels;
using BTechHaar.Models.API.Request;
using BTechHaar.Models.API.Response;
using BTechHaar.Models.Models.API.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Data.Repository
{
    public interface IAccountRepository
    {
        Task<LoginResponse> CheckValidLogin(LoginRequest request);
        Task<SignUpResponse> RegisterUser(SignupRequest request);
        Task<EmailVerifiedResponse> VerifyEmail(int userId);
    }


    public class AccountRepository : IAccountRepository
    {
        private readonly BTechDBContext _context;
        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };


        public AccountRepository(BTechDBContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> CheckValidLogin(LoginRequest request)
        {
            var user = await (from s in _context.Users
                              join d in _context.UserDevices on s.UserId equals d.UserId
                              where // s.MPin == request.MPin && 
                              s.EmailID == request.EmailID
                              select new LoginResponse()
                              {
                                  EmailId = s.EmailID,
                                  DeviceId = d.DeviceId,
                                  IsValidUser = true,
                                  IsEmailVerified = s.EmailVerified,
                                  ErrorMessage = string.Empty,
                                  OTPText = GenerateRandomOTP(6, saAllowedCharacters),
                                  UserId = s.UserId,
                                  FullName = s.FullName,
                                  MobileNumber = s.MobileNumber
                              }).FirstOrDefaultAsync();

            if (user == null)
            {
                user = new LoginResponse()
                {
                    OTPText = "",
                    IsValidUser = false,
                    ErrorMessage = "User not found. Kindly Register."
                };
                return user;
            }
            else if (!user.IsEmailVerified)
            {
                user.OTPText = GenerateRandomOTP(6, saAllowedCharacters);
                user.EmailId = request.EmailID;
                user.IsValidUser = false;
                user.UserId = user.UserId;
                user.ErrorMessage = "Email not verified. Kindly verify the OTP.";
                return user;
            }

            return user;
        }

        public async Task<SignUpResponse> RegisterUser(SignupRequest request)
        {
            var users = await _context.Users.ToListAsync();
            var isEmailExist = users.Where(x => x.EmailID == request.EmailID && x.EmailVerified).Any();
            if (isEmailExist)
            {
                return new SignUpResponse()
                {
                    IsEmailVerified = true,
                    ErrorMessage = "Email already exist. Please login."
                };
            }

            var emailExist = users.Where(x => x.EmailID == request.EmailID && !x.EmailVerified);
            if (emailExist.Any())
            {
                return new SignUpResponse()
                {
                    EmailId = request.EmailID,
                    OTPText = GenerateRandomOTP(6, saAllowedCharacters),
                    ErrorMessage = "Email already exist. Verify with OTP",
                    UserId = emailExist.FirstOrDefault().UserId
                };
            }
            var isMobileNumberExist = users.Where(x => x.MobileNumber == request.MobileNumber).Any();
            if (isMobileNumberExist)
            {
                return new SignUpResponse()
                {
                    ErrorMessage = "Mobile Number already exist"
                };
            }
            var isUserAlreadyExist = users.Where(x => x.EmailID == request.EmailID && x.MobileNumber == request.MobileNumber && x.EmailVerified).Any();
            if (isUserAlreadyExist)
            {
                return new SignUpResponse()
                {
                    IsEmailVerified = true,
                    ErrorMessage = "Account already exist. Kindly Login."
                };
            }
            var isEmailNotVerified = users.FirstOrDefault(x => x.EmailID == request.EmailID && x.MobileNumber == request.MobileNumber && !x.EmailVerified);
            if (isEmailNotVerified != null)
            {
                return new SignUpResponse()
                {
                    OTPText = GenerateRandomOTP(6, saAllowedCharacters),
                    EmailId = request.EmailID,
                    UserId = isEmailNotVerified.UserId
                };
            }
            Users newUser = new Users()
            {
                EmailID = request.EmailID,
                MobileNumber = request.MobileNumber,
                FullName = request.FullName,
                // MPin = request.MPin,
                EmailVerified = false
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            UserDevice device = new UserDevice()
            {
                UserId = newUser.UserId,
                DeviceId = request.DeviceId,
                DeviceType = request.DeviceType
            };

            await _context.UserDevices.AddAsync(device);
            await _context.SaveChangesAsync();

            return new SignUpResponse()
            {
                EmailId = request.EmailID,
                OTPText = GenerateRandomOTP(6, saAllowedCharacters),
                UserId = newUser.UserId,
            };

        }

        public async Task<EmailVerifiedResponse> VerifyEmail(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user != null)
            {
                user.EmailVerified = true;
                await _context.SaveChangesAsync();
                return new EmailVerifiedResponse()
                {
                    IsValidUser = true,
                    UserId = user.UserId
                };
            }
            else { return new EmailVerifiedResponse() { IsValidUser = false }; }
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }

            return sOTP;

        }

    }
}
