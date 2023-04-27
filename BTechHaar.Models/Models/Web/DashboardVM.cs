using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Models.Models.Web
{
    public class DashboardVM
    {
        public List<UserCountVM> userCountVM { get; set; }
        public List<LogCountVM> logCountVM { get; set; }
    }
}
