using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTechHaar.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BTechHaar.Data.Context
{
    public class BTechDBContext : DbContext
    {
        public BTechDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }

    }
}
