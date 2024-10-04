using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("tbl_user");

            entity.HasIndex(e => e.PhoneNumber, "PhoneNumber_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "UserId_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasMaxLength(60);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserRole).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
