using System;
using System.Collections.Generic;

namespace CarRental.Data.Entities
{
	public class Vehicle
	{
		public int VehicleId { get; set; }
		public int CategoryId { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public int Year { get; set; }
		public string Color { get; set; }
		public string LicensePlate { get; set; }
		public int Mileage { get; set; }
		public string FuelType { get; set; }
		public string TransmissionType { get; set; }
		public int SeatingCapacity { get; set; }
		public string ImageUrl { get; set; }
		public string Status { get; set; }
		public decimal DailyRate { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Navigation properties
		public virtual VehicleCategory Category { get; set; }
		public virtual ICollection<Booking> Bookings { get; set; }
		public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
	}
}