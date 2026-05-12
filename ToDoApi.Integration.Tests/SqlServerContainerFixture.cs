using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using ToDoApi.Features.Common;

//using Microsoft.EntityFrameworkCore;

public class SqlServerContainerFixture
{
    public MsSqlContainer DbContainer { get; private set; }

    public async Task StartAsync()
    {
        DbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("P@ssw0rd123")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Express")
            .Build();

        await DbContainer.StartAsync();

        await RunMigrationsAsync(DbContainer.GetConnectionString());
    }

    public static async Task RunMigrationsAsync(string connectionString)
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseSqlServer(connectionString)
            .Options;

        using var context = new ToDoContext(options);
        await context.Database.MigrateAsync();
    }

    public async Task StopAsync()
    {
        await DbContainer.DisposeAsync();
    }
}
