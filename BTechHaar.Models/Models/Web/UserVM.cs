using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Models.Models.Web
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public bool EmailVerified { get; set; } = false;
    }
}
