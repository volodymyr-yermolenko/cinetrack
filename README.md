# CineTrack - Backend API

A REST API for a 🔗 [**pet project**](https://cinetrack-ui.vercel.app/movies) (tracking movies and cinema information), built with C# and ASP.NET Core.


## 🏗️ Architecture

CineTrack follows clean architecture principles to ensure separation of concerns and maintainability:

### Project Structure

- **CineTrack.Domain** - Core business entities and enums
- **CineTrack.App** - Application logic, use cases, DTOs, and business rules
- **CineTrack.Infrastructure** - Data access, repositories, and database persistence
- **CineTrack.Api** - REST API endpoints, controllers, and HTTP configuration

## 🚀 Deployment

The API is hosted on **Microsoft Azure**:
- **Service**: Azure App Service
- **Database**: Azure SQL Database

## 📖 API Documentation

🔗 [**View Swagger Documentation**](https://cinetrack-g3cufqgbd2erh6ad.swedencentral-01.azurewebsites.net/swagger)

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core, C#
  - CQRS pattern with MediatR
  - AutoMapper
  - Entity Framework Core
- **Database**: Azure SQL Database
- **Architecture Pattern**: Clean Architecture / Layered Architecture

## 📋 Prerequisites

To run this project locally, you'll need:

- .NET 6.0 or higher
- Visual Studio 2022 or JetBrains Rider
- SQL Server IDE

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/volodymyr-yermolenko/cinetrack.git
cd cinetrack
for running, select the profile "CineTrack.Api: http"
