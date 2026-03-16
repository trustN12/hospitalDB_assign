using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HospitalDb.Models;

public partial class HospitalDBContext : DbContext
{
    public HospitalDBContext(DbContextOptions<HospitalDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Did).HasName("PK__Doctor__C031221830D2B7E4");

            entity.ToTable("Doctor");

            entity.Property(e => e.Did).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Specialisation)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__Patient__C5705938DA7D8B80");

            entity.ToTable("Patient");

            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Did)
                .HasConstraintName("FK__Patient__Did__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
