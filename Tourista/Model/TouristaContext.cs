using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Tourista;

public partial class TouristaContext : DbContext
{
    public TouristaContext()
    {
    }

    public TouristaContext(DbContextOptions<TouristaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=tourista", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PRIMARY");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.TourId, "tour_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("booking_date");
            entity.Property(e => e.BookingParticipants)
                .HasDefaultValueSql("'1'")
                .HasColumnName("booking_participants");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'")
                .HasColumnName("booking_status");
            entity.Property(e => e.BookingTotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("booking_total_price");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tour).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("bookings_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("bookings_ibfk_1");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PRIMARY");

            entity.ToTable("cities");

            entity.HasIndex(e => e.RegionName, "region_id");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("city_name");
            entity.Property(e => e.RegionName)
                .HasMaxLength(100)
                .HasColumnName("region_name");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("PRIMARY");

            entity.ToTable("tours");

            entity.HasIndex(e => e.CityId, "city_id");

            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.TourCurrentParticipants)
                .HasDefaultValueSql("'0'")
                .HasColumnName("tour_current_participants");
            entity.Property(e => e.TourDescription)
                .HasMaxLength(255)
                .HasColumnName("tour_description");
            entity.Property(e => e.TourEnd).HasColumnName("tour_end");
            entity.Property(e => e.TourImage)
                .HasMaxLength(100)
                .HasColumnName("tour_image");
            entity.Property(e => e.TourIsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("tour_is_active");
            entity.Property(e => e.TourMaxParticipants).HasColumnName("tour_max_participants");
            entity.Property(e => e.TourName)
                .HasMaxLength(255)
                .HasColumnName("tour_name");
            entity.Property(e => e.TourPrice)
                .HasPrecision(10, 2)
                .HasColumnName("tour_price");
            entity.Property(e => e.TourStart).HasColumnName("tour_start");

            entity.HasOne(d => d.City).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("tours_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserEmail, "user_email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(45)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(45)
                .HasColumnName("user_first_name");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(45)
                .HasColumnName("user_last_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(45)
                .HasColumnName("user_password");
            entity.Property(e => e.UserPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("user_phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
