# fixmycity Docker workflows

This project now uses Entity Framework Core with SQL Server.

## Files

- `compose.dev.yaml`: local development database only (app runs on your machine with local `dotnet` SDK)
- `compose.yaml`: production-like container run (app + SQL Server)

## 1) Fast local development (local SDK + containerized DB)

Start SQL Server:

```bash
docker compose -f compose.dev.yaml up -d
```

Run the API locally:

```bash
dotnet watch run --project fixmycity.csproj
```

Stop DB when done:

```bash
docker compose -f compose.dev.yaml down
```

## 2) Production-like run (both app and DB in containers)

```bash
docker compose -f compose.yaml up --build -d
```

Stop:

```bash
docker compose -f compose.yaml down
```

## Environment and connection string

- SQL Server exposed on host `localhost:14333`
- App reads `ConnectionStrings:Default`
- In container mode, compose injects `ConnectionStrings__Default` using `sqlserver` service name

You can override SA password at runtime:

```bash
MSSQL_SA_PASSWORD='YourStrong!Passw0rd' docker compose -f compose.dev.yaml up -d
```

## EF Core migrations

Install tool once (if needed):

```bash
dotnet tool install --global dotnet-ef
```

Create migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply migration:

```bash
dotnet ef database update
```

