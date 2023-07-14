using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using object_oriented_practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_practice.Data
{
    internal class DataContextEF :DbContext
    {

        private IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
                /*.ToTable("Computer", "TutorialAppSchema");*/
        }

    }
}
