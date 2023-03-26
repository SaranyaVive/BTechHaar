using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Models.Models.API.Request
{
    public class UserLogRequest
    {
        public int UserId { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogDescription { get; set; } = "";
        public int LogType { get; set; } = 0; /* enum LogType */
        public int? UserDeviceId { get; set; }
        public string FileName { get; set; } = "";
        public string FileSize { get; set; } = "";
        public string Contact { get; set; } = "";
    }
}
