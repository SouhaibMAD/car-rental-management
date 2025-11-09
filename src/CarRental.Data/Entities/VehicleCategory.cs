using System.Collections.Generic;

namespace CarRental.Data.Entities
{
    public class VehicleCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }

        // Navigation properties
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}