using Microsoft.EntityFrameworkCore;
using CarRental.Data.Entities;
using System;

namespace CarRental.Data
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer Configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.LicenseNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.LicenseNumber).IsUnique();
            });

            // VehicleCategory Configuration
            modelBuilder.Entity<VehicleCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(10,2)");

                entity.HasIndex(e => e.CategoryName).IsUnique();
            });

            // Vehicle Configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleId);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FuelType).IsRequired().HasMaxLength(20);
                entity.Property(e => e.TransmissionType).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Available");
                entity.Property(e => e.DailyRate).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Mileage).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.LicensePlate).IsUnique();
                entity.HasIndex(e => e.Status);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Vehicles)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Booking Configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.PickupLocation).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DropoffLocation).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DailyRate).HasColumnType("decimal(10,2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Reserved");
                entity.Property(e => e.BookingDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.VehicleId);
                entity.HasIndex(e => new { e.StartDate, e.EndDate });

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Bookings)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehicle)
                    .WithMany(v => v.Bookings)
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Check constraint: EndDate must be after StartDate
                entity.HasCheckConstraint("CK_Bookings_Dates", "[EndDate] > [StartDate]");
            });

            // Payment Configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TransactionId).HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.PaymentDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.BookingId);

                entity.HasOne(e => e.Booking)
                    .WithMany(b => b.Payments)
                    .HasForeignKey(e => e.BookingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // MaintenanceRecord Configuration
            modelBuilder.Entity<MaintenanceRecord>(entity =>
            {
                entity.HasKey(e => e.MaintenanceId);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Cost).HasColumnType("decimal(10,2)");
                entity.Property(e => e.ServiceProvider).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Vehicle)
                    .WithMany(v => v.MaintenanceRecords)
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Seed Data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Vehicle Categories
            modelBuilder.Entity<VehicleCategory>().HasData(
                new VehicleCategory { CategoryId = 1, CategoryName = "Economy", Description = "Budget-friendly compact cars", PricePerDay = 30.00m },
                new VehicleCategory { CategoryId = 2, CategoryName = "Sedan", Description = "Comfortable mid-size sedans", PricePerDay = 50.00m },
                new VehicleCategory { CategoryId = 3, CategoryName = "SUV", Description = "Spacious sport utility vehicles", PricePerDay = 80.00m },
                new VehicleCategory { CategoryId = 4, CategoryName = "Luxury", Description = "Premium luxury vehicles", PricePerDay = 150.00m },
                new VehicleCategory { CategoryId = 5, CategoryName = "Van", Description = "Large passenger vans", PricePerDay = 90.00m }
            );

            // Seed Sample Vehicles
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    VehicleId = 1,
                    CategoryId = 1,
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    Color = "White",
                    LicensePlate = "ABC-1234",
                    Mileage = 15000,
                    FuelType = "Petrol",
                    TransmissionType = "Automatic",
                    SeatingCapacity = 5,
                    DailyRate = 30.00m,
                    Status = "Available",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Vehicle
                {
                    VehicleId = 2,
                    CategoryId = 2,
                    Brand = "Honda",
                    Model = "Accord",
                    Year = 2023,
                    Color = "Black",
                    LicensePlate = "XYZ-5678",
                    Mileage = 8000,
                    FuelType = "Hybrid",
                    TransmissionType = "Automatic",
                    SeatingCapacity = 5,
                    DailyRate = 50.00m,
                    Status = "Available",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            // Seed Admin User (Password should be hashed in production)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Email = "admin@carrental.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEDummyHashForDemo123", // Change this!
                    FullName = "System Administrator",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}