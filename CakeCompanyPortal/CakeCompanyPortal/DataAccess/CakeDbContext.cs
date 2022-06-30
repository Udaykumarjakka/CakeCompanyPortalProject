using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.DataAccess
{
    public class CakeDbContext : DbContext
    {
        public CakeDbContext() : base("CakeComapnyDBEntities") { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(u => u.ProductID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

        }

    }
}
