using BTechHaar.Data.Repository;
using BTechHaar.Models.Models.API.Request;

namespace BTechHaar.Web.Services
{
    public interface IUserLogService
    {
        Task AddUserLog(UserLogRequest request);
        Task AddUserLogs(List<UserLogRequest> request);
    }

    public class UserLogService : IUserLogService
    {
        private readonly IUserLogRepository _userLogRepository;

        public UserLogService(IUserLogRepository userLogRepository)
        {
            _userLogRepository = userLogRepository;
        }

        public async Task AddUserLog(UserLogRequest request)
        {
            await _userLogRepository.AddUserLog(request);
        }

        public async Task AddUserLogs(List<UserLogRequest> request)
        {
            await _userLogRepository.AddUserLogs(request);
        }
    }
}
