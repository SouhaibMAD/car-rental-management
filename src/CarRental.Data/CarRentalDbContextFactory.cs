using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRental.Data
{
    public class CarRentalDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
    {
        public CarRentalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();

            // Connection string pour les migrations
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CarRentalDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new CarRentalDbContext(optionsBuilder.Options);
        }
    }
}