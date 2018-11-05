using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuiceTelegramBot.Core.Model
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<OrderDb> Orders { get; set; }
        public DbSet<JuiceDb> Juices { get; set; }
    }
}
