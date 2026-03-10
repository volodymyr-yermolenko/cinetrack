# CineTrack - Backend API

A modern REST API for tracking movies and cinema information, built with C# and ASP.NET Core.

## 🏗️ Architecture

CineTrack follows a **clean, layered architecture** to ensure separation of concerns and maintainability:

### Project Structure

- **CineTrack.Domain** - Core business entities and enums
- **CineTrack.App** - Application logic, use cases, DTOs, and business rules
- **CineTrack.Infrastructure** - Data access, repositories, and database persistence
- **CineTrack.Api** - REST API endpoints, controllers, and HTTP configuration

## 🚀 Deployment

The API is hosted on **Microsoft Azure**:
- **Service**: Azure App Service
- **Database**: Azure SQL Database
- **Region**: Sweden Central

## 📖 API Documentation

Interactive API documentation is available via Swagger:

🔗 [**View Swagger Documentation**](https://cinetrack-g3cufqgbd2erh6ad.swedencentral-01.azurewebsites.net/swagger)

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core
- **Language**: C#
- **ORM**: Entity Framework Core
- **Database**: Azure SQL Database
- **Architecture Pattern**: Clean Architecture / Layered Architecture

## 📋 Prerequisites

To run this project locally, you'll need:

- .NET 6.0 or higher
- Visual Studio 2022 or Visual Studio Code
- SQL Server / Azure SQL Database connection string

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/volodymyr-yermolenko/cinetrack.git
cd cinetrack
