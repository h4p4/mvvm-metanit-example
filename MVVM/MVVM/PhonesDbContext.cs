using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVVM.Models;

namespace MVVM
{
    public partial class PhonesDbContext : DbContext
    {
        public PhonesDbContext()
        {
        }

        public PhonesDbContext(DbContextOptions<PhonesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Phone> Phones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Helper.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.Company).HasMaxLength(50);

                entity.Property(e => e.Price).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
