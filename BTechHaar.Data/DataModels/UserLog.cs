using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Data.DataModels
{
    public class UserLog
    {
        [Key]
        public int UserLogId { get; set; }
        public int UserId { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogDescription { get; set; } = "";
        public int LogType { get; set; } = 0; /* enum LogType */
        public int? UserDeviceId { get; set; }
        public string FileName { get; set; } = "";
        public string FileSize { get; set; } = "";
        public string Contact { get; set; } = "";



        public Users User { get; set; }
        public UserDevice UserDevice { get; set; }
    }
}
