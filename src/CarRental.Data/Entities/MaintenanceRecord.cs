using System;

namespace CarRental.Data.Entities
{
    public class MaintenanceRecord
    {
        public int MaintenanceId { get; set; }
        public int VehicleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string ServiceProvider { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Vehicle Vehicle { get; set; }
    }
}