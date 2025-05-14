# Online Newspaper (Báo Điện Tử)

A modern web platform for managing and reading news articles, built with ASP.NET Core MVC, Entity Framework Core, and SQL Server/SQLite.

## Features

- User registration and login (with role-based access: Admin & User)
- Post, edit, and delete news articles
- Category management (create, edit, delete categories)
- Admin approval workflow for news articles
- Vietnamese language user interface
- Secure password hashing and session management
- Responsive, mobile-friendly design

## Technology Stack

- **Backend:** ASP.NET Core MVC
- **ORM:** Entity Framework Core
- **Database:** SQL Server (default) or SQLite (for local development)
- **Frontend:** Razor Views, Bootstrap
- **Authentication:** Session-based

## Getting Started

### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQLite for local/testing)

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/lethemanh/online-newspaper.git
   cd online-newspaper
   ```

2. **Install all project dependencies:**
   ```bash
   dotnet restore
   ```
   This will install all required NuGet packages for the project, including DotNetEnv.

3. **Install front-end libraries with LibMan:**

   The project uses [LibMan (Library Manager)](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/) to manage Bootstrap and Bootstrap Icons for styling.

   **LibMan CLI is a .NET local tool, not a NuGet package. It cannot be added to the .csproj and will NOT be installed by `dotnet restore`.**

   - To install and use LibMan CLI as a local tool (recommended for teamwork and CI/CD):
     ```bash
     dotnet new tool-manifest # Only needed once per repo
     dotnet tool install Microsoft.Web.LibraryManager.Cli
     dotnet tool restore
     dotnet tool run libman restore
     ```

   If you do not see the folder `wwwroot/lib` after running the above, check for errors and ensure LibMan CLI is on your PATH.

3. **Configure environment variables:**
   - Copy `.env.example` to `.env` and adjust the values as needed:
     ```bash
     cp .env.example .env
     ```
   - Set `DB_PROVIDER` to `SqlServer` or `Sqlite`.
   - Set the appropriate connection string (`SQLSERVER_CONNECTION_STRING` or `SQLITE_CONNECTION_STRING`). The password in these connection strings is for your database, not the admin account.
   - Optionally, set `ASPNETCORE_URLS` for a custom service port.

4. **Apply migrations and create the database:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```
   The app will be available at the port specified in your `.env` (default: `http://localhost:5046`).

6. **First Admin Account:**
   - On first run, an admin account is created automatically:
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
