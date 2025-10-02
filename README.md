### Sehaty Plus (Clean Architecture)

Sehaty Plus is a modular, maintainable Web API built with .NET 9 and Clean Architecture. It separates concerns across Presentation (API), Application, Domain, and Infrastructure layers, using MediatR for CQRS-style request handling, FluentValidation for validation, ASP.NET Identity for user management, and JWT for authentication.

---

## Solution Structure

- `Sehaty-Plus` (Presentation)
  - API controllers, DI for presentation, exception handling, OpenAPI.
- `Sehaty-Plus.Application` (Application)
  - Use cases (commands/queries), pipeline behaviors, DTOs, interfaces, JWT services, Result type.
- `Sehaty-Plus.Domain` (Domain)
  - Entities, value objects, and domain primitives.
- `Sehaty-Plus.Infrastructure` (Infrastructure)
  - EF Core `ApplicationDbContext`, Identity, JWT configuration, implementations of application interfaces, migrations.

---

## Implemented Features

- **Authentication (JWT + Refresh Tokens)**
  - Login: issue access token and refresh token
  - Refresh access token using a valid refresh token
  - Revoke refresh token
- **Users & Identity**
  - ASP.NET Identity integrated with EF Core
- **Specializations Module** (secured via `[Authorize]`)
  - List all specializations
  - Get specialization by id
  - Create specialization
  - Update specialization
  - Delete specialization
  - Toggle specialization activation
- **Application Layer**
  - MediatR for request/response handling
  - FluentValidation with `ValidationBehavior`
  - `Result`/`Result<T>` primitives for clear success/failure flows
- **Infrastructure**
  - SQL Server configuration with resilient retries
  - JWT bearer authentication and token validation
  - OpenAPI exposed in Development environment
- **Observability & Errors**
  - Serilog request logging
  - Centralized exception handling via `GlobalExceptionHandler`

---

## Planned / Roadmap

- Registration and account lifecycle
  - User registration
  - Email confirmation flow
  - Resend confirmation email
  - Password reset (request/reset)
- Role-based authorization policies
- Seed data and initial admin setup
- More modules/entities beyond Specializations
- Unit and integration tests
- CI/CD pipeline
- Dockerization and container orchestration
- API documentation enhancements (Swagger UI with JWT auth, examples)
- CORS configuration and environment hardening

---

## Requirements

- .NET SDK 9
- SQL Server (local or remote)

---

## Configuration

Update `Sehaty-Plus/appsettings.json` (and/or `appsettings.Development.json`) with:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=SehatyPlus;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "Key": "super-secret-key-change-me",
    "ExpirationInMinutes": 60
  },
  "Serilog": {
    "Using": ["Serilog.Sinks.Console"],
    "WriteTo": [{ "Name": "Console" }]
  }
}
```

Notes:

- Section name `Jwt` must match the application’s `JwtOptions.SectionName`.
- Ensure the connection string points to a reachable SQL Server instance.

---

## Database

Migrations are managed in `Sehaty-Plus.Infrastructure`. To create the database:

```bash
dotnet tool restore
dotnet ef database update --project Sehaty-Plus.Infrastructure --startup-project Sehaty-Plus
```

If you need to add a migration (example):

```bash
dotnet ef migrations add Add_New_Feature --project Sehaty-Plus.Infrastructure --startup-project Sehaty-Plus
dotnet ef database update --project Sehaty-Plus.Infrastructure --startup-project Sehaty-Plus
```

---

## Running the API

From the solution root:

```bash
dotnet restore
dotnet build
dotnet run --project Sehaty-Plus/Sehaty-Plus.csproj
```

In Development, OpenAPI is exposed via the built-in endpoint registered by `MapOpenApi()`.

---

## Authentication

The API uses JWT bearer authentication. After login, pass the access token in the `Authorization` header:

```http
Authorization: Bearer <access_token>
```

Refresh tokens are stored with the user (Identity) and can be rotated via the refresh endpoint.

---

## API Endpoints (Current)

Base URL examples below assume local development over HTTPS.

### Auth

- POST `https://localhost:<port>/Auth`

  - Purpose: Login and issue tokens
  - Sample request body:
    ```json
    {
      "email": "user@example.com",
      "password": "P@ssw0rd!"
    }
    ```
  - Response: `AuthResponse` with `accessToken`, `expiresIn`, `refreshToken`, and user info

- POST `https://localhost:<port>/Auth/refresh`

  - Purpose: Exchange a valid refresh token for a new access token
  - Sample request body:
    ```json
    {
      "token": "<access_token>",
      "refreshToken": "<refresh_token>"
    }
    ```

- POST `https://localhost:<port>/Auth/revoke`
  - Purpose: Revoke an active refresh token
  - Sample request body:
    ```json
    {
      "token": "<access_token>",
      "refreshToken": "<refresh_token>"
    }
    ```

### Specializations (requires Authorization)

- GET `https://localhost:<port>/api/Specializations`
- GET `https://localhost:<port>/api/Specializations/{id}`
- POST `https://localhost:<port>/api/Specializations`
  - Body example:
    ```json
    {
      "name": "Cardiology",
      "description": "Heart and cardiovascular care"
    }
    ```
- PUT `https://localhost:<port>/api/Specializations/{id}`
  - Body example:
    ```json
    {
      "name": "Cardiology",
      "description": "Updated description"
    }
    ```
- DELETE `https://localhost:<port>/api/Specializations/{id}`
- PATCH `https://localhost:<port>/api/Specializations/{id}/toggle-active`

---

## Development Notes

- Exception handling is centralized by `GlobalExceptionHandler` and mapped via `AddExceptionHandler`/`UseExceptionHandler`.
- `ValidationBehavior` runs before handlers, returning validation errors with problem details.
- `ApplicationDbContext` updates audit fields (`CreatedById`, `UpdatedById`, `UpdatedOn`) using the current user from `HttpContext`.
- Query execution helpers are abstracted behind `IQueryExecuter`.

---

## Roadmap (High Level)

- Full auth lifecycle (register, confirm email, reset password)
- Role/Policy-based authorization and endpoint protection
- Comprehensive tests (unit/integration) and CI
- Swagger UI with OAuth2/JWT support and request/response examples
- Health checks and readiness/liveness probes
- Dockerfile + compose for API + SQL Server
- Seed data and sample users

---

## License

This project is provided under a permissive license. Update this section with your chosen license (e.g., MIT) as needed.
