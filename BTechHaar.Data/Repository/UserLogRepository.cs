using BTechHaar.Data.Context;
using BTechHaar.Data.DataModels;
using BTechHaar.Models.Models.API.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Data.Repository
{
    public interface IUserLogRepository
    {
        Task AddUserLog(UserLogRequest request);
        Task AddUserLogs(List<UserLogRequest> request);
    }

    public class UserLogRepository : IUserLogRepository
    {
        private readonly BTechDBContext _context;

        public UserLogRepository(BTechDBContext context)
        {
            _context = context;
        }

        public async Task AddUserLog(UserLogRequest request)
        {
            UserLog log = new UserLog()
            {

                UserId = request.UserId,
                LogDate = request.LogDate,
                LogDescription = request.LogDescription,
                LogType = request.LogType,
                UserDeviceId = request.UserDeviceId,
                FileName = request.FileName,
                FileSize = request.FileSize,
                Contact = request.Contact,
            };

            await _context.UserLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserLogs(List<UserLogRequest> request)
        {
            List<UserLog> newLogs = new List<UserLog>();
            foreach (var item in request)
            {
                UserLog log = new UserLog()
                {

                    UserId = item.UserId,
                    LogDate = item.LogDate,
                    LogDescription = item.LogDescription,
                    LogType = item.LogType,
                    UserDeviceId = item.UserDeviceId,
                    FileName = item.FileName,
                    FileSize = item.FileSize,
                    Contact = item.Contact,
                };
                newLogs.Add(log);
            }


            await _context.UserLogs.AddRangeAsync(newLogs);
            await _context.SaveChangesAsync();
        }
    }
}
