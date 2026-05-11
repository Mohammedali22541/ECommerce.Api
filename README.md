# ECommerce.Api

ASP.NET Core Web API backend for an e-commerce system.  
The solution follows a layered architecture and includes product catalog, basket, orders, and identity/authentication modules.

## Overview

This repository contains a .NET 8 e-commerce backend built with:

- RESTful API controllers
- SQL Server persistence via Entity Framework Core
- Redis-backed basket/cache features
- ASP.NET Core Identity + JWT token generation
- Swagger/OpenAPI for API exploration (Development environment)

## Key Features

- **Product APIs**
  - List products with query parameters and pagination response model
  - Get product details by ID
  - List product brands and product types
  - Redis response caching on product listing endpoint
- **Basket APIs**
  - Get basket by ID
  - Create/update basket
  - Delete basket
- **Order APIs**
  - Create order (authorized)
  - Get all current user orders (authorized)
  - Get order by ID (authorized)
  - Get delivery methods
- **Authentication APIs**
  - Register
  - Login
  - Check if email exists
  - Get/update current user address (authorized)
- **Infrastructure**
  - Automatic migration + seeding at startup for both store and identity databases
  - Custom exception handling middleware

## Tech Stack

- **Framework:** ASP.NET Core Web API (.NET 8)
- **Language:** C#
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Cache:** Redis (StackExchange.Redis)
- **Auth:** ASP.NET Core Identity + JWT Bearer
- **Object Mapping:** AutoMapper
- **API Docs:** Swashbuckle / Swagger

## Solution Structure

```text
ECommerce.sln
├── Ecommerce.Api                  # Startup project (DI, middleware, configuration)
├── ECommerce.Presentation         # API controllers and attributes
├── Ecommerce.Services             # Business logic/services/specifications/mapping
├── Ecommerce.Services.Abstraction # Service interfaces
├── ECommerce.Persistence          # EF Core contexts, repositories, migrations, seeders
├── Ecommerce.Domain               # Domain entities and contracts
└── ECommerce.Shared               # DTOs and shared response models
```

## Prerequisites

- .NET 8 SDK
- SQL Server instance
- Redis instance
- (Optional) Visual Studio 2022 / VS Code + C# extensions

## Local Development Setup

From repository root:

```bash
dotnet restore ECommerce.sln
dotnet build ECommerce.sln
dotnet run --project Ecommerce.Api/Ecommerce.Api.csproj
```

Default launch settings include:

- `https://localhost:7296`
- `http://localhost:5002`

Swagger UI (Development):

- `https://localhost:7296/swagger`

## Configuration

Main configuration files:

- `Ecommerce.Api/appsettings.json`
- `Ecommerce.Api/appsettings.Development.json`

Important settings:

- `ConnectionStrings:DefaultConnection` (store database)
- `ConnectionStrings:IdentityConnection` (identity database)
- `ConnectionStrings:RedisConnection`
- `JWTOptions:SecretKey`
- `JWTOptions:Issuer`
- `JWTOptions:Audience`
- `URLs:BaseUrl`

> Recommended: override secrets and environment-specific values via environment variables or `dotnet user-secrets` for local development.

## Database, Migrations, and Seeding

This API applies pending migrations and seeds data automatically on startup:

- Store DB migration + seed (`DataIntializier`)
- Identity DB migration + seed (`IdentityDataIntializer`)

Seed sources include JSON files under:

- `ECommerce.Persistence/Data/DataSeed/JsonFiles`

Manual EF migration commands (if needed):

```bash
dotnet ef database update \
  --project ECommerce.Persistence/ECommerce.Persistence.csproj \
  --startup-project Ecommerce.Api/Ecommerce.Api.csproj \
  --context StoreDbContext

dotnet ef database update \
  --project ECommerce.Persistence/ECommerce.Persistence.csproj \
  --startup-project Ecommerce.Api/Ecommerce.Api.csproj \
  --context StoreIdentityDbContext
```

## API Documentation (Swagger/OpenAPI)

Swagger is enabled in `Development` environment.

1. Run the API.
2. Open `/swagger`.
3. Use the **Authorize** button and provide a Bearer JWT token for protected endpoints.

## Testing

There are currently **no dedicated test projects** in the repository.

Basic verification:

```bash
dotnet build ECommerce.sln
dotnet test ECommerce.sln
```

## Docker

No `Dockerfile` or `docker-compose` configuration is currently included in this repository.

## CI/CD

GitHub Actions is enabled with a workflow named **Copilot cloud agent** in this repository.

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Make focused changes with clear commit messages.
4. Ensure build passes locally.
5. Open a pull request.

## License

No license specified.
