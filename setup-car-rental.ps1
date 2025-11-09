# Car Rental Solution Setup Script
# Run this in PowerShell from your desired parent directory

Write-Host "=== Car Rental Solution Setup ===" -ForegroundColor Cyan
Write-Host ""

# Create solution
Write-Host "Creating solution..." -ForegroundColor Yellow
dotnet new sln -n CarRentalSolution

# Create src directory
Write-Host "Creating directory structure..." -ForegroundColor Yellow
New-Item -ItemType Directory -Path "src" -Force | Out-Null
New-Item -ItemType Directory -Path "docs" -Force | Out-Null
New-Item -ItemType Directory -Path "deploy" -Force | Out-Null

# Create projects
Write-Host "Creating projects..." -ForegroundColor Yellow

Write-Host "  - CarRental.Data (Class Library)" -ForegroundColor Gray
dotnet new classlib -n CarRental.Data -o src/CarRental.Data -f net7.0

Write-Host "  - CarRental.Core (Class Library)" -ForegroundColor Gray
dotnet new classlib -n CarRental.Core -o src/CarRental.Core -f net7.0

Write-Host "  - CarRental.Infrastructure (Class Library)" -ForegroundColor Gray
dotnet new classlib -n CarRental.Infrastructure -o src/CarRental.Infrastructure -f net7.0

Write-Host "  - CarRental.Admin.WinForms (Windows Forms)" -ForegroundColor Gray
dotnet new winforms -n CarRental.Admin.WinForms -o src/CarRental.Admin.WinForms -f net7.0

Write-Host "  - CarRental.Admin.WPF (WPF)" -ForegroundColor Gray
dotnet new wpf -n CarRental.Admin.WPF -o src/CarRental.Admin.WPF -f net7.0

Write-Host "  - CarRental.Web (ASP.NET Core MVC)" -ForegroundColor Gray
dotnet new mvc -n CarRental.Web -o src/CarRental.Web -f net7.0

Write-Host "  - CarRental.API (Web API)" -ForegroundColor Gray
dotnet new webapi -n CarRental.API -o src/CarRental.API -f net7.0

Write-Host "  - CarRental.Tests (xUnit Tests)" -ForegroundColor Gray
dotnet new xunit -n CarRental.Tests -o src/CarRental.Tests -f net7.0

# Add projects to solution
Write-Host ""
Write-Host "Adding projects to solution..." -ForegroundColor Yellow
dotnet sln add src/CarRental.Data/CarRental.Data.csproj
dotnet sln add src/CarRental.Core/CarRental.Core.csproj
dotnet sln add src/CarRental.Infrastructure/CarRental.Infrastructure.csproj
dotnet sln add src/CarRental.Admin.WinForms/CarRental.Admin.WinForms.csproj
dotnet sln add src/CarRental.Admin.WPF/CarRental.Admin.WPF.csproj
dotnet sln add src/CarRental.Web/CarRental.Web.csproj
dotnet sln add src/CarRental.API/CarRental.API.csproj
dotnet sln add src/CarRental.Tests/CarRental.Tests.csproj

# Add project references
Write-Host ""
Write-Host "Setting up project dependencies..." -ForegroundColor Yellow

# Infrastructure depends on Core and Data
Write-Host "  - Infrastructure → Core, Data" -ForegroundColor Gray
dotnet add src/CarRental.Infrastructure/CarRental.Infrastructure.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.Infrastructure/CarRental.Infrastructure.csproj reference src/CarRental.Data/CarRental.Data.csproj

# WinForms depends on Core and Infrastructure
Write-Host "  - WinForms → Core, Infrastructure" -ForegroundColor Gray
dotnet add src/CarRental.Admin.WinForms/CarRental.Admin.WinForms.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.Admin.WinForms/CarRental.Admin.WinForms.csproj reference src/CarRental.Infrastructure/CarRental.Infrastructure.csproj

# WPF depends on Core and Infrastructure
Write-Host "  - WPF → Core, Infrastructure" -ForegroundColor Gray
dotnet add src/CarRental.Admin.WPF/CarRental.Admin.WPF.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.Admin.WPF/CarRental.Admin.WPF.csproj reference src/CarRental.Infrastructure/CarRental.Infrastructure.csproj

# Web depends on Core and Infrastructure
Write-Host "  - Web → Core, Infrastructure" -ForegroundColor Gray
dotnet add src/CarRental.Web/CarRental.Web.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.Web/CarRental.Web.csproj reference src/CarRental.Infrastructure/CarRental.Infrastructure.csproj

# API depends on Core and Infrastructure
Write-Host "  - API → Core, Infrastructure" -ForegroundColor Gray
dotnet add src/CarRental.API/CarRental.API.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.API/CarRental.API.csproj reference src/CarRental.Infrastructure/CarRental.Infrastructure.csproj

# Tests depend on Core and Infrastructure
Write-Host "  - Tests → Core, Infrastructure" -ForegroundColor Gray
dotnet add src/CarRental.Tests/CarRental.Tests.csproj reference src/CarRental.Core/CarRental.Core.csproj
dotnet add src/CarRental.Tests/CarRental.Tests.csproj reference src/CarRental.Infrastructure/CarRental.Infrastructure.csproj

# Install NuGet packages for Data project
Write-Host ""
Write-Host "Installing Entity Framework Core packages..." -ForegroundColor Yellow
dotnet add src/CarRental.Data/CarRental.Data.csproj package Microsoft.EntityFrameworkCore
dotnet add src/CarRental.Data/CarRental.Data.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add src/CarRental.Data/CarRental.Data.csproj package Microsoft.EntityFrameworkCore.Tools

# Install EF Core for Infrastructure
Write-Host "Installing packages for Infrastructure..." -ForegroundColor Yellow
dotnet add src/CarRental.Infrastructure/CarRental.Infrastructure.csproj package Microsoft.EntityFrameworkCore
dotnet add src/CarRental.Infrastructure/CarRental.Infrastructure.csproj package System.Data.SqlClient

# Create initial folder structure
Write-Host ""
Write-Host "Creating folder structure..." -ForegroundColor Yellow

# Data project folders
New-Item -ItemType Directory -Path "src/CarRental.Data/Entities" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Data/Configurations" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Data/Migrations" -Force | Out-Null

# Core project folders
New-Item -ItemType Directory -Path "src/CarRental.Core/Models" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Core/Interfaces" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Core/Services" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Core/DTOs" -Force | Out-Null

# Infrastructure project folders
New-Item -ItemType Directory -Path "src/CarRental.Infrastructure/Repositories" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Infrastructure/Data" -Force | Out-Null
New-Item -ItemType Directory -Path "src/CarRental.Infrastructure/Scripts" -Force | Out-Null

# Create README files
Write-Host ""
Write-Host "Creating documentation files..." -ForegroundColor Yellow

$readmeContent = @"
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
2. Run EF Core migrations: ``dotnet ef database update --project src/CarRental.Data``
3. Build solution: ``dotnet build``
4. Run desired project

## Features (MVP)

- Car catalog management
- Customer CRUD operations
- Booking flow (reserve → pay → rent → return)
- Admin CRUD operations
- Reports generation

## Non-Functional Requirements

- Data integrity
- Security
- Concurrency handling
- Responsive web UI
"@

$readmeContent | Out-File -FilePath "README.md" -Encoding UTF8

# Create docs structure
New-Item -ItemType Directory -Path "docs/architecture" -Force | Out-Null
New-Item -ItemType Directory -Path "docs/api" -Force | Out-Null
New-Item -ItemType Directory -Path "docs/database" -Force | Out-Null

# Create .gitignore
$gitignoreContent = @"
## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Aa][Rr][Mm]/
[Aa][Rr][Mm]64/
bld/
[Bb]in/
[Oo]bj/
[Ll]og/

# Visual Studio cache/options directory
.vs/

# NuGet Packages
*.nupkg
**/packages/*
!**/packages/build/

# Database files
*.mdf
*.ldf
*.ndf

# Connection strings (if sensitive)
**/appsettings.Development.json
"@

$gitignoreContent | Out-File -FilePath ".gitignore" -Encoding UTF8

Write-Host ""
Write-Host "=== Setup Complete! ===" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Open CarRentalSolution.sln in Visual Studio" -ForegroundColor White
Write-Host "2. Update connection strings in appsettings.json" -ForegroundColor White
Write-Host "3. Create your entity models in CarRental.Data/Entities" -ForegroundColor White
Write-Host "4. Create DbContext in CarRental.Data" -ForegroundColor White
Write-Host "5. Run: dotnet ef migrations add InitialCreate --project src/CarRental.Data" -ForegroundColor White
Write-Host ""
Write-Host "Solution structure:" -ForegroundColor Cyan
Write-Host "  /CarRentalSolution" -ForegroundColor White
Write-Host "    /src" -ForegroundColor White
Write-Host "      /CarRental.Data" -ForegroundColor Gray
Write-Host "      /CarRental.Core" -ForegroundColor Gray
Write-Host "      /CarRental.Infrastructure" -ForegroundColor Gray
Write-Host "      /CarRental.Admin.WinForms" -ForegroundColor Gray
Write-Host "      /CarRental.Admin.WPF" -ForegroundColor Gray
Write-Host "      /CarRental.Web" -ForegroundColor Gray
Write-Host "      /CarRental.API" -ForegroundColor Gray
Write-Host "      /CarRental.Tests" -ForegroundColor Gray
Write-Host "    /docs" -ForegroundColor White
Write-Host "    /deploy" -ForegroundColor White
Write-Host ""