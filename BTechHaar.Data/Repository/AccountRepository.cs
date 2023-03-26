using BTechHaar.Data.Context;
using BTechHaar.Data.DataModels;
using BTechHaar.Models.API.Request;
using BTechHaar.Models.API.Response;
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
        Task VerifyEmail(int userId);
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
                              where s.MPin == request.MPin && d.DeviceId == request.DeviceId
                              select new LoginResponse()
                              {
                                  DeviceId = d.DeviceId,
                                  IsValidUser = true
                              }).FirstOrDefaultAsync();
            if (user == null)
            {
                return new LoginResponse() { DeviceId = request.DeviceId, IsValidUser = false };
            }

            return user;
        }

        public async Task<SignUpResponse> RegisterUser(SignupRequest request)
        {
            var users = await _context.Users.ToListAsync();
            var isEmailExist = users.Where(x => x.EmailID == request.EmailID).Any();
            if (isEmailExist)
            {
                return new SignUpResponse()
                {
                    ErrorMessage = "Email already exist"
                };
            }
            var isMobileNumberExist = users.Where(x => x.MobileNumber == request.MobileNumber).Any();
            if (isMobileNumberExist)
            {
                return new SignUpResponse()
                {
                    ErrorMessage = "MobileNumber already exist"
                };
            }
            var isUserAlreadyExist = users.Where(x => x.EmailID == request.EmailID && x.MobileNumber == request.MobileNumber && x.EmailVerified).Any();
            if (isUserAlreadyExist)
            {
                return new SignUpResponse()
                {
                    ErrorMessage = "User already exist"
                };
            }
            var isEmailNotVerified = users.FirstOrDefault(x => x.EmailID == request.EmailID && x.MobileNumber == request.MobileNumber && !x.EmailVerified);
            if (isEmailNotVerified != null)
            {
                return new SignUpResponse()
                {
                    OTPText = GenerateRandomOTP(4, saAllowedCharacters),
                    UserId = isEmailNotVerified.UserId
                };
            }
            Users newUser = new Users()
            {
                EmailID = request.EmailID,
                MobileNumber = request.MobileNumber,
                FullName = request.FullName,
                MPin = request.MPin,
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
                OTPText = GenerateRandomOTP(4, saAllowedCharacters),
                UserId = newUser.UserId,
            };

        }

        public async Task VerifyEmail(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user != null)
            {
                user.EmailVerified = true;
                await _context.SaveChangesAsync();
            }
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
