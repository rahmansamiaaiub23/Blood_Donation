using System;
using System.Collections.Generic;
using Blood_Donation.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace Blood_Donation.EF;

public partial class BloodBankDbContext : DbContext
{
    public BloodBankDbContext()
    {
    }

    public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>(entity =>
        {
            entity.ToTable("Donation");

            entity.Property(e => e.CampName).HasMaxLength(50);
            entity.Property(e => e.DonationDate)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Donor).WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Donation");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.ToTable("Donor");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
