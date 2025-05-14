using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

// Design-time factory for EF Core CLI support
public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
{
    public SqlServerDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
        var connStr = config.GetConnectionString("SqlServer") ?? "Server=localhost;Database=BaoDienTu;User Id=sa;Password=YourStrong!Passw0rd;";
        optionsBuilder.UseSqlServer(connStr);
        return new SqlServerDbContext(optionsBuilder.Options);
    }
}

public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
{
    public SqliteDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
        var connStr = config.GetConnectionString("Sqlite") ?? "Data Source=BaoDienTu.db";
        optionsBuilder.UseSqlite(connStr);
        return new SqliteDbContext(optionsBuilder.Options);
    }
}
