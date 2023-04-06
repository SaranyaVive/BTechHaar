using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Models.Models.API.Response
{
    public class EmailVerifiedResponse
    {
        public bool IsValidUser { get; set; }
        public int UserId { get; set; }
    }
}
