﻿using BTechHaar.Data.Repository;
using BTechHaar.Models.API.Request;
using BTechHaar.Models.API.Response;
using BTechHaar.Models.Models.API.Response;

namespace BTechHaar.Main.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> CheckValidLogin(LoginRequest request);
        Task<SignUpResponse> RegisterUser(SignupRequest request);
        Task<EmailVerifiedResponse> VerifyEmail(int userId);
    }


    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;

        public AccountService(IAccountRepository accountRepository, IEmailService emailService, ISMSService smsService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<LoginResponse> CheckValidLogin(LoginRequest request)
        {
            var usercheck = await _accountRepository.CheckValidLogin(request);
            if (usercheck.IsValidUser)
            {
                await _emailService.SendOTPEmail(usercheck.EmailId, usercheck.OTPText);
                await _smsService.SendOTP(usercheck.MobileNumber, usercheck.OTPText);
            }
            else if (!string.IsNullOrEmpty(usercheck.OTPText) && !string.IsNullOrEmpty(usercheck.EmailId))
            {
                await _emailService.SendOTPEmail(usercheck.EmailId, usercheck.OTPText);
                await _smsService.SendOTP(usercheck.MobileNumber, usercheck.OTPText);
            }
            return usercheck;
        }

        public async Task<SignUpResponse> RegisterUser(SignupRequest request)
        {
            var userRegister = await _accountRepository.RegisterUser(request);
            if (!string.IsNullOrEmpty(userRegister.OTPText) && !string.IsNullOrEmpty(userRegister.EmailId))
            {
                var errorMailSending = await _emailService.SendOTPEmail(userRegister.EmailId, userRegister.OTPText);
                var errorSMSSending = _smsService.SendOTP(userRegister.MobileNumber, userRegister.OTPText);
                if (!string.IsNullOrEmpty(errorMailSending))
                {

                }
            }
            return userRegister;
        }

        public async Task<EmailVerifiedResponse> VerifyEmail(int userId)
        {
            return await _accountRepository.VerifyEmail(userId);
        }
    }
}
