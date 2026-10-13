# ResolveDesk

ResolveDesk is an ASP.NET Core MVC help desk MVP built from the supplied UI prototype. It keeps the prototype's dark navigation, blue accent system, card-led dashboard, queues, and ticket-detail conversation pattern, while adding real Identity-backed authorization and EF Core persistence.

## Run locally

```powershell
dotnet run
```

The first launch creates `resolvedesk.db` and seeds these development-only accounts (password: `Demo!12345`):

| Role | Account |
| --- | --- |
| Super Admin | admin@resolvedesk.local |
| Agent | agent@resolvedesk.local |
| End user | user@resolvedesk.local |

## Delivered MVP

- ASP.NET Core 8 MVC, EF Core and ASP.NET Core Identity
- Role-based ticket visibility (end users only see their own tickets)
- Ticket creation, searchable queues, detail pages, replies and staff-only internal notes
- Centralized validated ticket status transitions and audit activity records
- Priority-based resolution SLA deadline calculation
- Seeded departments, accounts and tickets; `/health` endpoint

SQLite is selected for a zero-configuration local demonstration; the data model uses EF Core and can be moved to SQL Server by adding the SQL Server provider and changing the DbContext provider/connection string.

## Verification

`dotnet build -c Release --no-restore` completed successfully with 0 warnings and 0 errors.
