using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hahn.ApplicatonProcess.May2020.Data.Models
{
    public partial class HahnContext : DbContext
    {
        public HahnContext()
        {
        }

        public HahnContext(DbContextOptions<HahnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=(localdb)\\xdule;Database=Hahn;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CountryOfOrigin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("EMailAddress")
                    .HasMaxLength(100);

                entity.Property(e => e.FamiliyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
