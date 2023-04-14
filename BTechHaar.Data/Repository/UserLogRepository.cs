using BTechHaar.Data.Context;
using BTechHaar.Data.DataModels;
using BTechHaar.Models.Models.API.Request;
using BTechHaar.Models.Models.Web;
using Microsoft.EntityFrameworkCore;
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
        Task<List<UserLogsVM>> GetUserLogs();
        Task<List<UserLogsVM>> GetUserLogs(int userId);
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
                LogDate = DateTime.Parse(request.LogDate),
                LogDescription = request.LogDescription,
                LogType = request.LogType,
                FileName = request.FileName ?? "-",
                FileSize = request.FileSize ?? "-",
                Contact = request.Contact ?? "-",
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
                    LogDate = DateTime.Parse(item.LogDate),
                    LogDescription = item.LogDescription,
                    LogType = item.LogType,
                    FileName = item.FileName ?? "-",
                    FileSize = item.FileSize ?? "-",
                    Contact = item.Contact ?? "-",
                };
                newLogs.Add(log);
            }


            await _context.UserLogs.AddRangeAsync(newLogs);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserLogsVM>> GetUserLogs()
        {
            var logs = await (from s in _context.UserLogs
                              join u in _context.Users on s.UserId equals u.UserId  
                              select new UserLogsVM()
                              { 
                                  UserId = u.UserId,
                                  Contact = s.Contact, 
                                  EmailID = u.EmailID,
                                  EmailVerified = u.EmailVerified,
                                  FileName = s.FileName,
                                  FileSize = s.FileSize,
                                  FullName = u.FullName,
                                  LogDate = s.LogDate,
                                  LogDescription = s.LogDescription,
                                  LogType = s.LogType,
                                  MobileNumber = u.MobileNumber,
                                  MPin = u.MPin,
                                  Password = u.Password,
                              }).ToListAsync();
            return logs;
        }

        public async Task<List<UserLogsVM>> GetUserLogs(int userId)
        {
            var logs = await (from s in _context.UserLogs
                              join u in _context.Users on s.UserId equals u.UserId
                              join d in _context.UserDevices on s.UserId equals d.UserId
                              where s.UserDeviceId == d.UserId && s.UserId == userId
                              select new UserLogsVM()
                              {
                                  UserDeviceId = d.UserDeviceId,
                                  DeviceId = d.DeviceId,
                                  UserId = u.UserId,
                                  Contact = s.Contact,
                                  DeviceType = d.DeviceType,
                                  EmailID = u.EmailID,
                                  EmailVerified = u.EmailVerified,
                                  FileName = s.FileName,
                                  FileSize = s.FileSize,
                                  FullName = u.FullName,
                                  LogDate = s.LogDate,
                                  LogDescription = s.LogDescription,
                                  LogType = s.LogType,
                                  MobileNumber = u.MobileNumber,
                                  MPin = u.MPin,
                                  Password = u.Password,
                              }).ToListAsync();
            return logs;
        }
    }
}
