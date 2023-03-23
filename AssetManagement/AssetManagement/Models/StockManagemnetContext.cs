﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssetManagement.Models
{
    public partial class StockManagemnetContext : DbContext
    {
        public StockManagemnetContext()
        {
        }

        public StockManagemnetContext(DbContextOptions<StockManagemnetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; } = null!;
        public virtual DbSet<BorrowingAsset> BorrowingAssets { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conf.GetConnectionString("MusicStore"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.AssetName).HasMaxLength(250);

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .HasColumnName("image");

                entity.Property(e => e.Specification).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Assets__Category__398D8EEE");
            });

            modelBuilder.Entity<BorrowingAsset>(entity =>
            {
                entity.ToTable("BorrowingAsset");

                entity.Property(e => e.BorrowDate).HasColumnType("date");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.RetrurnDate).HasColumnType("date");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.BorrowingAssets)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK__Borrowing__Asset__2C3393D0");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.BorrowingAssets)
                    .HasForeignKey(d => d.BorrowerId)
                    .HasConstraintName("FK__Borrowing__Borro__2D27B809");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(250);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Username).HasMaxLength(250);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
