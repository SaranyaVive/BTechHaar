using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Data.DataModels
{
    public class UserDevice
    {
        public int UserDeviceId { get; set; }
        public int UserId { get; set; }
        public int DeviceType { get; set; } = 0; /*  enum DeviceType */
        public string DeviceId { get; set; } = "";
    }
}
