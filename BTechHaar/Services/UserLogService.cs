using BTechHaar.Data.Repository;
using BTechHaar.Models.Models.API.Request;
using BTechHaar.Models.Models.Web;

namespace BTechHaar.Main.Services
{
    public interface IUserLogService
    {
        Task AddUserLog(UserLogRequest request);
        Task AddUserLogs(List<UserLogRequest> request);
        Task<List<UserLogsVM>> GetUserLogs();
        Task<List<UserLogsVM>> GetUserLogs(int userId);
        Task<List<UserVM>> GetUserList();
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

        public async Task<List<UserLogsVM>> GetUserLogs()
        {
            return await _userLogRepository.GetUserLogs();
        }

        public async Task<List<UserLogsVM>> GetUserLogs(int userId)
        {
            return await _userLogRepository.GetUserLogs(userId); 
        }

        public async Task<List<UserVM>> GetUserList()
        {
            return await _userLogRepository.GetUserList();
        }
    }
}
