# FixMyCity

> A backend service for reporting, tracking, and managing civic issues.

FixMyCity is a .NET API that provides the foundation for a platform where citizens can report local problems—such as damaged roads, broken streetlights, or waste collection issues—and follow their resolution. It stores users, reports, media, comments, and report-status history in SQL Server.

> **Project status:** the user-registration and current-user endpoints are implemented. Civil-report endpoints are planned; the underlying data model and database migrations are already in place.

## Highlights

- Built with ASP.NET Core 10 and Entity Framework Core
- SQL Server persistence with migrations applied automatically at startup
- Designed to sit behind an authentication gateway
- Gateway user context is available throughout the request lifecycle
- Docker Compose setup for local and containerized development

## Technology

| Area | Technology |
| --- | --- |
| Framework | ASP.NET Core 10 |
| Data access | Entity Framework Core 10 |
| Database | SQL Server 2022 |
| Containers | Docker and Docker Compose |
| API specification | OpenAPI (Development environment) |

## Quick start

### Prerequisites

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)
- Docker and Docker Compose

### 1. Start SQL Server

```bash
docker compose -f compose.dev.yaml up -d
```

This starts SQL Server at `localhost:14333` and keeps its data in the `sqlserver_data` Docker volume.

### 2. Run the API

```bash
dotnet restore
dotnet run
```

The API starts at `http://localhost:5181`. Pending Entity Framework migrations run automatically when the application starts.

### 3. Check the connection

All requests require the user context normally supplied by the authentication gateway:

```bash
curl http://localhost:5181/api/city/Testing/test \
  -H 'X-User-Id: citizen-001' \
  -H 'X-User-Role: citizen'
```

Expected response:

```text
hello world | user=citizen-001 | role=citizen
```

## Running with Docker

The full Docker setup starts the API on port `8080` and SQL Server on port `14333`.

```bash
docker network create microservices-network
docker compose up --build
```

The `microservices-network` is external by design, so it must exist before starting the full stack.

## API reference

Base path: `/api/city`

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/Testing/test` | Confirms that the API receives the gateway user context. |
| `POST` | `/Auth/register` | Creates a user from the gateway headers. |
| `GET` | `/Auth/me` | Returns the currently authenticated user. |

In the Development environment, the OpenAPI document is available at:

```text
/openapi/v1.json
```

### Gateway headers

FixMyCity expects a trusted upstream gateway to authenticate the client and send user details as HTTP headers.

| Header | Description | Used by |
| --- | --- | --- |
| `X-User-Id` | Unique user identifier | Every endpoint |
| `X-User-Role` | User role, for example `citizen` | Every endpoint |
| `X-User-Email` | User email address | Registration |
| `X-User-Name` | User first name | Registration |
| `X-User-LastName` | User last name | Registration |

Requests without `X-User-Id` receive `401 Missing X-User-Id`.

> **Security note:** these headers are trusted by the application. Deploy FixMyCity behind a gateway that authenticates requests and removes any client-supplied identity headers before forwarding them.

### Register a user

`POST /api/city/Auth/register`

User details are read from the gateway headers. The current request body is empty.

```bash
curl -X POST http://localhost:5181/api/city/Auth/register \
  -H 'Content-Type: application/json' \
  -H 'X-User-Id: citizen-001' \
  -H 'X-User-Role: citizen' \
  -H 'X-User-Email: citizen@example.com' \
  -H 'X-User-Name: Ada' \
  -H 'X-User-LastName: Lovelace' \
  -d '{}'
```

### Get the current user

`GET /api/city/Auth/me`

```bash
curl http://localhost:5181/api/city/Auth/me \
  -H 'X-User-Id: citizen-001' \
  -H 'X-User-Role: citizen'
```

Example response:

```json
{
  "message": null,
  "data": {
    "id": "citizen-001",
    "role": "citizen",
    "firstName": "Ada",
    "lastName": "Lovelace"
  },
  "timestamp": "2026-01-01T00:00:00Z"
}
```

## Architecture

```text
Client
  │
  ▼
Authentication gateway ── adds X-User-* headers
  │
  ▼
FixMyCity API
  ├── Middleware: builds the current-user context
  ├── Controllers: HTTP endpoints
  ├── Services: application logic
  ├── Repositories: data access
  └── Entity Framework Core
        │
        ▼
    SQL Server
```

## Domain model

The current database schema includes the following entities:

| Entity | Purpose |
| --- | --- |
| `User` | A platform user supplied by the authentication gateway. |
| `CivilReport` | A civic issue with title, description, location, address, and status. |
| `ReportImage` | An image or video associated with a report. |
| `ReportComment` | A comment left on a report by a user. |
| `ReportStatusHistory` | An audit trail of status changes. |

Reports support the following statuses: `IN_PROGRESS`, `UNDER_REVIEW`, `RESOLVED`, and `REJECTED`.

## Configuration

The default connection string is configured for the development Compose database:

```text
Server=localhost,14333;Database=fixmycity;User Id=sa;Password=FixMyCity!123;Encrypt=False;TrustServerCertificate=True
```

For another database, override it with the `ConnectionStrings__Default` environment variable:

```bash
ConnectionStrings__Default='Server=localhost,14333;Database=fixmycity;User Id=sa;Password=<your-password>;Encrypt=False;TrustServerCertificate=True' dotnet run
```

For a real deployment, provide database credentials through a secret manager or deployment environment variables rather than committing them to source control.

## Project structure

```text
Controllers/     HTTP endpoints
Data/            Entity Framework DbContext
DTOs/            API request and response contracts
Enums/           Shared domain constants
MiddleWares/     Gateway user-context middleware
Migrations/      Entity Framework database migrations
Models/          Domain entities
Repositories/    Database queries and commands
Security/        Current-request user context
Services/        Application logic
```

## Development

Build the project:

```bash
dotnet build
```

The repository does not currently contain an automated test project. New functionality should include automated coverage, particularly for gateway-authenticated request flows and report authorization.

## Roadmap

- [ ] Create, list, and view civic reports
- [ ] Upload report images and videos
- [ ] Add comments and status updates
- [ ] Apply role-based authorization for municipal staff
- [ ] Add automated tests and CI validation

## Contributing

1. Create a branch from `main`.
2. Keep the change focused and follow the existing project structure.
3. Run `dotnet build` and relevant tests before opening a pull request.
4. Describe the change and how you verified it in the pull request.

## License

No license has been specified for this repository.
