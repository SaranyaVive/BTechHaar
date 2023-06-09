﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTechHaar.Data.DataModels
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; } = "";
        public string MPin { get; set; } = "";
        public bool EmailVerified { get; set; } = false; 
    }
}
