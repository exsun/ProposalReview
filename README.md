# README: Initializing Proposal Review with a .NET Server and Blazor WebAssembly Client with PostgreSQL

This guide will walk you through setting up a .NET-based server, a Blazor WebAssembly client, and integrating PostgreSQL as the database.

---

## Prerequisites

1. **Install .NET SDK**: [Download .NET](https://dotnet.microsoft.com/download) (ensure .NET version >= 6.0).
2. **Install PostgreSQL**: [Download PostgreSQL](https://www.postgresql.org/download/).
3. **Install a Database GUI Tool** (optional): e.g., pgAdmin, DBeaver.
4. **Install Node.js**: For managing front-end dependencies.

---

## Folder Structure

```# README: Initializing a .NET Server and Blazor WebAssembly Client with PostgreSQL

This guide will walk you through setting up a .NET-based server, a Blazor WebAssembly client, and integrating PostgreSQL as the database.

---

## Prerequisites

1. **Install .NET SDK**: [Download .NET](https://dotnet.microsoft.com/download) (ensure .NET version >= 6.0).
2. **Install PostgreSQL**: [Download PostgreSQL](https://www.postgresql.org/download/).
3. **Install a Database GUI Tool** (optional): e.g., pgAdmin, DBeaver.
4. **Install Node.js**: For managing front-end dependencies.

---

## Folder Structure

```
ProjectRoot/
  ├── Server/          # .NET Web API Server
  ├── Client/          # Blazor WebAssembly Client
  ├── Shared/          # Shared code
  └── README.md        # This file
```

---

## Steps

### 1. Initialize the Solution

```bash
mkdir ProjectRoot && cd ProjectRoot

# Create a new .NET solution
dotnet new sln --name ProjectName

# Create a server project
mkdir Server
cd Server

dotnet new webapi --name Server

# Add server project to the solution
cd ..
dotnet sln add Server/Server.csproj

# Create a Blazor WebAssembly project
mkdir Client
cd Client

dotnet new blazorwasm --name Client --hosted

# Add client project to the solution
cd ..
dotnet sln add Client/Client.csproj
```

---

### 2. Install PostgreSQL Dependencies

#### Add PostgreSQL NuGet Package to the Server

```bash
cd Server

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

### 3. Configure PostgreSQL

#### Add Connection String to `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword"
  }
}
```

#### Create the Database Context

In the `Server` project, add a folder named `Data`.

Create a file `ApplicationDbContext.cs` in the `Data` folder:

```csharp
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<YourModel> YourModels { get; set; } // Example DbSet
    }
}
```

#### Register the Database Context in `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.Run();
```

---

### 4. Link the Client to the Server

Ensure the Blazor WebAssembly client uses the hosted server by default. When you created the project with `--hosted`, it already set up the Client to interact with the Server through the `/api` path.

To confirm, open `Client/wwwroot/appsettings.json`:

```json
{
  "ApiBaseUrl": "https://localhost:5001/api"
}
```

---

### 5. Run the Application

Run both the Server and the Client.

```bash
# Run the server
cd Server

dotnet run

# Run the client
cd ../Client

dotnet run
```

Access the application in your browser:

- Blazor WebAssembly Client: [https://localhost:5001](https://localhost:5001)
- API Endpoints (e.g., Swagger): [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

### 6. Migrations and Database Updates

To update the database schema using Entity Framework:

```bash
# Add a migration
cd Server

dotnet ef migrations add InitialCreate

# Apply the migration

dotnet ef database update
```

---

## Notes

- **Environment Variables**: Use environment variables to store sensitive database credentials in production.
- **Deployment**: Consider Docker for containerization, especially for PostgreSQL.

---

## References

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core with PostgreSQL](https://learn.microsoft.com/en-us/ef/core/providers/npgsql/)
- [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)

ProjectRoot/
  ├── Server/          # .NET Web API Server
  ├── Client/          # Blazor WebAssembly Client
  ├── shared/          # Shared code (optional)
  └── README.md        # This file
```

---

## Steps

### 1. Initialize the Solution

```bash
mkdir ProjectRoot && cd ProjectRoot

# Create a new .NET solution
dotnet new sln --name ProjectName

# Create a server project
mkdir Server
cd Server

dotnet new webapi --name Server

# Add server project to the solution
cd ..
dotnet sln add Server/Server.csproj

# Create a Blazor WebAssembly project
mkdir Client
cd Client

dotnet new blazorwasm --name Client --hosted

# Add client project to the solution
cd ..
dotnet sln add Client/Client.csproj
```

---

### 2. Install PostgreSQL Dependencies

#### Add PostgreSQL NuGet Package to the Server

```bash
cd Server

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

### 3. Configure PostgreSQL

#### Add Connection String to `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword"
  }
}
```

#### Create the Database Context

In the `Server` project, add a folder named `Data`.

Create a file `ApplicationDbContext.cs` in the `Data` folder:

```csharp
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<YourModel> YourModels { get; set; } // Example DbSet
    }
}
```

#### Register the Database Context in `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.Run();
```

---

### 4. Link the Client to the Server

Ensure the Blazor WebAssembly client uses the hosted server by default. When you created the project with `--hosted`, it already set up the Client to interact with the Server through the `/api` path.

To confirm, open `Client/wwwroot/appsettings.json`:

```json
{
  "ApiBaseUrl": "https://localhost:5001/api"
}
```

---

### 5. Run the Application

Run both the Server and the Client.

```bash
# Run the server
cd Server

dotnet run

# Run the client
cd ../Client

dotnet run
```

Access the application in your browser:

- Blazor WebAssembly Client: [https://localhost:5001](https://localhost:5001)
- API Endpoints (e.g., Swagger): [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

### 6. Migrations and Database Updates

To update the database schema using Entity Framework:

```bash
# Add a migration
cd Server

dotnet ef migrations add InitialCreate

# Apply the migration

dotnet ef database update
```

---

## Notes

- **Environment Variables**: Use environment variables to store sensitive database credentials in production.
- **Deployment**: Consider Docker for containerization, especially for PostgreSQL.

---

## References

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core with PostgreSQL](https://learn.microsoft.com/en-us/ef/core/providers/npgsql/)
- [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
