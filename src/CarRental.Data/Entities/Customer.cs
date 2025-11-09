using System;
using System.Collections.Generic;

namespace CarRental.Data.Entities
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string LicenseNumber { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PasswordHash { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsActive { get; set; }

		// Navigation properties
		public virtual ICollection<Booking> Bookings { get; set; }
	}
}