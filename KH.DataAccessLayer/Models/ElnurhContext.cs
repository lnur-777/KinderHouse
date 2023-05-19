using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KH.DataAccessLayer.Models;

public partial class ElnurhContext : DbContext
{
    public ElnurhContext()
    {
    }

    public ElnurhContext(DbContextOptions<ElnurhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Pupil> Pupils { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=elnurh_; User Id=elnurh_;Password=1234;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("Lesson");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Pupil>(entity =>
        {
            entity.ToTable("Pupil");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.FatherName).HasMaxLength(50);
            entity.Property(e => e.FatherPhoneNum).HasMaxLength(50);
            entity.Property(e => e.MotherName).HasMaxLength(50);
            entity.Property(e => e.MotherPhoneNum).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Orientation)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.RegisterDate).HasColumnType("date");
            entity.Property(e => e.SurName).HasMaxLength(50);

            entity.HasOne(d => d.Sector).WithMany(p => p.Pupils)
                .HasForeignKey(d => d.SectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pupil_Sector");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.ToTable("Purchase");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Note).HasMaxLength(250);
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Pupil).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.PupilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_Pupil");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.ToTable("Sector");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.SurName).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("Worker");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FatherName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(250);
            entity.Property(e => e.RegisteredDate).HasColumnType("date");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SurName).HasMaxLength(50);

            entity.HasOne(d => d.Lesson).WithMany(p => p.Workers)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK_Worker_Lesson");

            entity.HasOne(d => d.Position).WithMany(p => p.Workers)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Worker_Position");

            entity.HasOne(d => d.Sector).WithMany(p => p.Workers)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK_Worker_Sector");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
