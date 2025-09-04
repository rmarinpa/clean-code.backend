# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 6 Clean Architecture solution implementing a customer management system. The solution follows the Clean Architecture pattern with clear separation of concerns across multiple layers.

## Build and Run Commands

- **Build solution**: `dotnet build Solucion_CleanCode.sln`
- **Run API**: `dotnet run --project CleanCode/CleanCode.API.csproj`
- **Restore packages**: `dotnet restore Solucion_CleanCode.sln`

## Architecture

The solution is organized into four main projects following Clean Architecture principles:

### Layer Structure
- **CleanCode.API** (Web Layer): Controllers, extensions, and API configuration
- **CleanCode.Application** (Application Layer): Services, DTOs, interfaces, and business logic
- **CleanCode.Core** (Domain Layer): Entities and domain models
- **CleanCode.Infraestructura** (Infrastructure Layer): Data access, Entity Framework context

### Key Architectural Patterns
- **Clean Architecture**: Dependencies flow inward toward the core domain
- **Dependency Injection**: Services registered via extension methods
- **AutoMapper**: For object-to-object mapping
- **MediatR**: For mediator pattern implementation (configured but not actively used)

## Technology Stack

- **.NET 6** with nullable reference types enabled
- **Entity Framework Core 7.0.3** with SQL Server
- **AutoMapper 12.0** for object mapping
- **Swagger/OpenAPI** for API documentation
- **Application Insights** for telemetry
- **Health Checks** at `/healtz` endpoint

## Configuration

- **Localization**: Default culture set to `es-CL` (Chilean Spanish)
- **Database**: SQL Server connection string named `DefaultConnection`
- **CORS**: Configured with exposed headers for pagination
- **User Secrets**: Enabled for API project

## Project Dependencies

- Core → (no dependencies)
- Infrastructure → Core
- Application → Core + Infrastructure
- API → Application + Core + Infrastructure

## Key Files

- `Program.cs`: Application startup and middleware configuration
- `ApplicationExtensions.cs`: Application layer service registration
- `InfraestructureExtensions.cs`: Infrastructure and database service registration
- `CleanCodeDbContext.cs`: Entity Framework database context