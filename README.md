# PartnerQuotes API

A clean, layered ASP.NET Core 8 Web API for registering and managing partner quotes.

- Clean architecture (Core / Infrastructure / API separation)
- RESTful design with custom exceptions and HTTP status code mapping
- Dependency injection and in-memory repository
- Unit tested service layer (xUnit + Moq)
- Swagger/OpenAPI documentation

---

## Project Structure

```
src/
  PartnerQuotes.Api/           # ASP.NET Core Web API (controllers, Program.cs)
  PartnerQuotes.Core/          # Domain models, interfaces, DTOs, exceptions
  PartnerQuotes.Infrastructure/ # In-memory repository implementation
tests/
  PartnerQuotes.Core.Tests/    # Unit tests for service layer
```

---

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

---

## Build

```bash
dotnet build
```

---

## Run

```bash
dotnet run --project src/PartnerQuotes.Api
```

The API will be available at `http://localhost:5111` by default.

---

## Swagger UI

Navigate to `http://localhost:5111/swagger` to explore and test the API interactively.

---

## Test

```bash
dotnet test
```

---

## Endpoints

| Method | Route            | Description                         |
| ------ | ---------------- | ----------------------------------- |
| POST   | /partners        | Register a new partner              |
| GET    | /partners        | List all partners (optional filter) |
| GET    | /partners/{guid} | Get a partner by ID                 |

### POST /partners

**Request Body:**

```json
{
  "name": "Acme Corp",
  "email": "acme@example.com",
  "phone": "555-0100"
}
```

**Responses:**

- `201 Created` – Partner created; includes `Location` header and API key in body.
- `400 Bad Request` – Validation failure (missing or invalid fields).
- `409 Conflict` – Email already registered.

### GET /partners

**Query Parameters:**

- `nameFilter` (optional) – Filter partners by partial name match.

**Responses:**

- `200 OK` – Array of partners (empty array if none found).

### GET /partners/{guid}

**Responses:**

- `200 OK` – Partner found.
- `404 Not Found` – No partner with that ID.

---

## Error Responses

All errors return a [ProblemDetails](https://datatracker.ietf.org/doc/html/rfc7807) response:

```json
{
  "status": 404,
  "title": "Partner not found",
  "detail": "Partner with ID '...' was not found.",
  "instance": "/partners/..."
}
```

---

## Example HTTP Requests

See [`src/PartnerQuotes.Api/PartnerQuotes.Api.http`](src/PartnerQuotes.Api/PartnerQuotes.Api.http) for sample requests covering all endpoints and error cases. These can be run directly in VS Code with the [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).
