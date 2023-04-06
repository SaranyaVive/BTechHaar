using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Models.Models.Web
{
    public class UserLogsVM
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; } = "";
        public string MPin { get; set; } = "";
        public bool EmailVerified { get; set; } = false;
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogDescription { get; set; } = "";
        public int LogType { get; set; } = 0; /* enum LogType */
        public int? UserDeviceId { get; set; }
        public string FileName { get; set; } = "";
        public string FileSize { get; set; } = "";
        public string Contact { get; set; } = "";
        public int DeviceType { get; set; } = 0; /*  enum DeviceType */
        public string DeviceId { get; set; } = "";
    }
}
