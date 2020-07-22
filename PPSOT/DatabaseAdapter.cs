using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PPSOT
{
    public class DatabaseAdapter : DbContext
    {
        public DbSet<BDTariff> tariff { get; set; }

        public DatabaseAdapter()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=94.29.18.215;Port=5432;Database=PPSOT;Username=misha;Password=2312");
        }
    }
}
