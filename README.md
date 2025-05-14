# Online Newspaper

A modern web platform for managing and reading news articles, built with ASP.NET Core MVC, Entity Framework Core, and SQL Server/SQLite.

## Features

- User registration and login (Admin & User roles)
- Create, edit, and delete news articles
- Category management
- Admin approval workflow for news
- Secure password hashing and session management
- Responsive, mobile-friendly design

## Technology Stack

- **Backend:** ASP.NET Core MVC
- **ORM:** Entity Framework Core
- **Database:** SQL Server (default) or SQLite (for local/testing)
- **Frontend:** Razor Views, Bootstrap
- **Authentication:** Session-based

## Getting Started

### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (optional, for production)
- SQLite (built-in, for local/testing)

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/lethemanh/online-newspaper.git
   cd online-newspaper
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Install front-end libraries with LibMan:**
   ```bash
   dotnet new tool-manifest # Only needed once per repo
   dotnet tool install Microsoft.Web.LibraryManager.Cli
   dotnet tool restore
   dotnet tool run libman restore
   ```
   If you do not see the folder `wwwroot/lib` after running the above, check for errors and ensure LibMan CLI is on your PATH.

4. **Configure environment variables:**
   - Copy `.env.example` to `.env` and adjust values as needed:
     ```bash
     cp .env.example .env
     ```
   - Set `DB_PROVIDER` to `SqlServer` or `Sqlite` to choose the database provider.

## Database Migration & Update

This project uses a single `AppDbContext` base class, with provider-specific subclasses: `SqlServerDbContext` for SQL Server and `SqliteDbContext` for SQLite. Migrations are managed separately for each provider.

### Creating a New Migration

- **For SQL Server:**
  ```bash
  dotnet ef migrations add <MigrationName> --context SqlServerDbContext --output-dir Migrations/SqlServer
  ```
- **For SQLite:**
  ```bash
  dotnet ef migrations add <MigrationName> --context SqliteDbContext --output-dir Migrations/Sqlite
  ```

### Applying Migrations to the Database

- **For SQL Server:**
  ```bash
  dotnet ef database update --context SqlServerDbContext
  ```
- **For SQLite:**
  ```bash
  dotnet ef database update --context SqliteDbContext
  ```

**Note:**
- You do not need to set a custom migrations assembly in `Program.cs`. The migrations are separated by output directory and context.
- Make sure your `.env` file is configured correctly before running migrations or starting the app.


This project uses a single `AppDbContext` base class, with provider-specific subclasses for SQL Server and SQLite. Migrations are managed separately for each provider.

### Generating Migrations

- **SQL Server:**
  ```bash
  dotnet ef migrations add <MigrationName> --context SqlServerDbContext --output-dir Migrations/SqlServer
  ```
- **SQLite:**
  ```bash
  dotnet ef migrations add <MigrationName> --context SqliteDbContext --output-dir Migrations/Sqlite
  ```

After generating or updating migrations, apply them to your database:

- **SQL Server:**
  ```bash
  dotnet ef database update --context SqlServerDbContext
  ```
- **SQLite:**
  ```bash
  dotnet ef database update --context SqliteDbContext
  ```

### Notes
- You do **not** need to specify a custom migrations assembly in `Program.cs`. The migrations are organized by output directory and context.
- Make sure your environment variables (`DB_PROVIDER`, connection strings, etc.) are set correctly in your `.env` file before running migrations or starting the app.

## Running the Application

```bash
   dotnet run
```
The app will be available at the port specified in your `.env` (default: `http://localhost:5046`).

### First Admin Account
On first run, an admin account is created automatically if none exists:
- **Username:** admin
- **Email:** admin@localhost
- **Password:** 123456

## Folder Structure

- `Controllers/` — MVC controllers for handling requests
- `Models/` — Data models and Entity Framework entities
- `Views/` — Razor view templates
- `wwwroot/` — Static files (CSS, JS, images)

## Notes
- All user-facing content is localized in Vietnamese.
- Only admin users can approve news and manage categories.
- Deleting a category is blocked if it has related news articles.

## License

This project is for educational and demonstration purposes.
