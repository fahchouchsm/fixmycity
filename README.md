# FixMyCity API

FixMyCity is a .NET 10 REST API for a civic-issue reporting platform. It persists users and the foundations for civil reports, attachments, comments, and report-status history in SQL Server.

At the moment, the exposed API focuses on receiving user identity from an API gateway, registering a user, and retrieving the current user. Report-management endpoints have not been implemented yet.

## Stack

- ASP.NET Core 10 Web API
- Entity Framework Core 10
- SQL Server 2022
- Docker Compose

## Requirements

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)
- Docker and Docker Compose (recommended for SQL Server)

## Run locally

1. Start SQL Server:

   ```bash
   docker compose -f compose.dev.yaml up -d
   ```

2. Restore, build, and run the API:

   ```bash
   dotnet restore
   dotnet run
   ```

   The development profile listens on `http://localhost:5181`. On startup, pending Entity Framework migrations are applied automatically.

The default development connection string targets `localhost,14333`, which is the host port configured by `compose.dev.yaml`. To use another database, override `ConnectionStrings__Default`:

```bash
ConnectionStrings__Default='Server=localhost,14333;Database=fixmycity;User Id=sa;Password=<your-password>;Encrypt=False;TrustServerCertificate=True' dotnet run
```

## Run with Docker

The full compose setup runs the API on port `8080` and SQL Server on host port `14333`.

```bash
docker network create microservices-network
docker compose up --build
```

`microservices-network` must be created first because the production compose file declares it as an external Docker network.

## Authentication context

This service expects an upstream gateway to pass the authenticated user in request headers. Every endpoint requires `X-User-Id`; requests without it receive `401 Missing X-User-Id`.

| Header | Purpose | Required by |
| --- | --- | --- |
| `X-User-Id` | User identifier | All endpoints |
| `X-User-Role` | User role | Gateway context |
| `X-User-Email` | User email | Registration |
| `X-User-Name` | First name | Registration |
| `X-User-LastName` | Last name | Registration |

The service currently trusts these gateway headers. Do not expose it directly to untrusted clients without putting a trusted authentication gateway in front of it.

## API

Base path: `/api/city`

### Check gateway context

`GET /Testing/test`

```bash
curl http://localhost:5181/api/city/Testing/test \
  -H 'X-User-Id: user-123' \
  -H 'X-User-Role: citizen'
```

### Register the current user

`POST /Auth/register`

The user data is read from gateway headers; the request body is currently empty.

```bash
curl -X POST http://localhost:5181/api/city/Auth/register \
  -H 'X-User-Id: user-123' \
  -H 'X-User-Role: citizen' \
  -H 'X-User-Email: citizen@example.com' \
  -H 'X-User-Name: Ada' \
  -H 'X-User-LastName: Lovelace' \
  -H 'Content-Type: application/json' \
  -d '{}'
```

### Get the current user

`GET /Auth/me`

```bash
curl http://localhost:5181/api/city/Auth/me \
  -H 'X-User-Id: user-123' \
  -H 'X-User-Role: citizen'
```

In the Development environment, the OpenAPI document is available at `/openapi/v1.json`.

## Project layout

```text
Controllers/    HTTP endpoints
Services/       Application logic
Repositories/   Data-access operations
Data/           Entity Framework DbContext
Models/         User and civil-report domain entities
DTOs/           Request and response data contracts
MiddleWares/    Gateway user-context middleware
Migrations/     Entity Framework database migrations
```

## Database model

The database contains users, civil reports, report images, report comments, and report-status history. `CivilReport` supports a title, description, geographic coordinates, optional address, status, images, comments, and status changes. These entities are ready for future endpoints but are not yet exposed through the API.

## Development notes

```bash
dotnet build
```

There is no automated test project in the repository yet. When adding endpoints, include tests and ensure a gateway-authenticated request path is covered.

## Contributing

1. Create a branch.
2. Make a focused change.
3. Run `dotnet build` and any relevant tests.
4. Open a pull request that explains the change and its verification.

## License

No license has been specified for this repository.
