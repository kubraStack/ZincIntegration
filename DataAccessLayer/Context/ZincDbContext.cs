using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class ZincDbContext : DbContext
    {
        public ZincDbContext(DbContextOptions<ZincDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; } // Order Table


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Orders");
        }

    }
}
