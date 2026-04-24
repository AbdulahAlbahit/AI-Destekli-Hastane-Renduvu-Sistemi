using System;
using System.Collections.Generic;
using Data_Accese_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Accese_Layer;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(SqlCon.con);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__APPOINTM__49B308C65E500E5D");

            entity.ToTable("APPOINTMENTS", "HOSPITAL_SYSTEM");

            entity.Property(e => e.AppointmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("APPOINTMENT_ID");
            entity.Property(e => e.ClinicId).HasColumnName("CLINIC_ID");
            entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");
            entity.Property(e => e.PatientId).HasColumnName("PATIENT_ID");
            entity.Property(e => e.TheDate).HasColumnName("THE_DATE");
            entity.Property(e => e.TheStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("THE_STATUS");
            entity.Property(e => e.TheTime).HasColumnName("THE_TIME");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK_APPOINTMENTS_CLINICS");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_APPOINTMENTS_DOCTORS");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_APPOINTMENTS_PATIENTS");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__CLINICS__7A56A460F09ACA09");

            entity.ToTable("CLINICS", "HOSPITAL_SYSTEM");

            entity.Property(e => e.ClinicId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CLINIC_ID");
            entity.Property(e => e.ClinicNumber).HasColumnName("CLINIC_NUMBER");
            entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");
            entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");

            entity.HasOne(d => d.Dept).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK_CLINICS_DEPARTMENTS");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__DEPARTME__512A59AC4D33B8B5");

            entity.ToTable("DEPARTMENTS", "HOSPITAL_SYSTEM");

            entity.Property(e => e.DeptId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DEPT_ID");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DEPT_NAME");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__DOCTORS__596ABDB06355F749");

            entity.ToTable("DOCTORS", "HOSPITAL_SYSTEM");

            entity.Property(e => e.DoctorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DOCTOR_ID");
            entity.Property(e => e.ClinicId).HasColumnName("CLINIC_ID");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOCTOR_NAME");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SPECIALIZATION");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FK_DOCTORS_CLINICS");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__PATIENTS__AA0B6068ECFD06AA");

            entity.ToTable("PATIENTS", "HOSPITAL_SYSTEM");

            entity.Property(e => e.PatientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PATIENT_ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.PatientName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PATIENT_NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
