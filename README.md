# Car Rental Solution

## Project Structure

- **CarRental.Data**: Entity Framework Core entities, DbContext, and migrations
- **CarRental.Core**: Business logic, services, interfaces, and DTOs
- **CarRental.Infrastructure**: Repository implementations, ADO.NET helpers, SQL scripts
- **CarRental.Admin.WinForms**: Windows Forms admin application
- **CarRental.Admin.WPF**: WPF admin application (optional)
- **CarRental.Web**: ASP.NET Core MVC customer-facing web application
- **CarRental.API**: REST API for modern frontend integrations
- **CarRental.Tests**: Unit and integration tests

## Getting Started

1. Update connection strings in appsettings.json files
2. Run EF Core migrations: `dotnet ef database update --project src/CarRental.Data`
3. Build solution: `dotnet build`
4. Run desired project

## Features (MVP)

- Car catalog management
- Customer CRUD operations
- Booking flow (reserve â†’ pay â†’ rent â†’ return)
- Admin CRUD operations
- Reports generation

## Non-Functional Requirements

- Data integrity
- Security
- Concurrency handling
- Responsive web UI
