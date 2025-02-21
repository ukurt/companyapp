using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Company.Infrastructure.Persistence
{
    public class CompanyContext : DbContext
    {
        public DbSet<Domain.Models.Company> Companies { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Company entity
            modelBuilder.Entity<Domain.Models.Company>(entity =>
            {
                entity.HasKey(c => c.Id); // Primary key
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.StockTicker).IsRequired().HasMaxLength(10);
                entity.Property(c => c.Exchange).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Isin).IsRequired().HasMaxLength(12);
                entity.Property(c => c.Website).HasMaxLength(200);

                // Unique constraint for ISIN
                entity.HasIndex(c => c.Isin).IsUnique();
            });
        }
    }
}
