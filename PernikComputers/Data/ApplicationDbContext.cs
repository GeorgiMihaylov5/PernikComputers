using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PernikComputers.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using PernikComputers.Models;

namespace PernikComputers.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<VideoCard> VideoCards { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<ComputerCase> ComputerCases { get; set; }


        public DbSet<Product> Product { get; set; }
        public DbSet<Computer> Computers { get; set; }

        //public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Computer>().HasKey(x => new
        //    {
        //        x.ProcessorId,
        //        x.MotherboardId,
        //        x.RamId,
        //        x.VideoCardId,
        //        x.PowerSupplyId,
        //        x.MemoryId,
        //        x.ComputerCaseId
        //    });
        //    base.OnModelCreating(builder);
        //}
    }
}
