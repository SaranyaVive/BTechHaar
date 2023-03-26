using BTechHaar.Data.Repository;
using BTechHaar.Models.API.Request;
using BTechHaar.Models.API.Response;

namespace BTechHaar.Web.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> CheckValidLogin(LoginRequest request);
        Task<SignUpResponse> RegisterUser(SignupRequest request);
        Task VerifyEmail(int userId);
    }


    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<LoginResponse> CheckValidLogin(LoginRequest request)
        {
            return await _accountRepository.CheckValidLogin(request);
        }

        public async Task<SignUpResponse> RegisterUser(SignupRequest request)
        {
            return await _accountRepository.RegisterUser(request);
        }

        public async Task VerifyEmail(int userId)
        {
            await _accountRepository.VerifyEmail(userId);
        }
    }
}
