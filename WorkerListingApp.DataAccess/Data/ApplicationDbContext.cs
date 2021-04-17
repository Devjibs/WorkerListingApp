using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerListingApp.Core.Models;

namespace WorkerListingApp.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Using Fluent Api for ignoring mapping of entity Employees to the company Table
            //Property Configurations
            modelBuilder.Entity<Company>().Ignore(t => t.Employees);


            //Adding as a foreign key
            modelBuilder.Entity<Employee>()
                .HasOne(c => c.Company).WithMany(e => e.Employees)
                .HasForeignKey(c => c.CompanyId);
        }
    }
}
