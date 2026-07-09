using Testcontainers.MsSql;

namespace ToDoApi.Integration.Tests;

[SetUpFixture]
public class SqlServerContainerFixture
{
    public MsSqlContainer? DbContainer { get; set; }

    public async Task StartAsync()
    {
        DbContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("P@ssw0rd123")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Express")
            .Build();

        await DbContainer.StartAsync();
    }

    public async Task StopAsync()
    {
        if (DbContainer != null)
        {
            await DbContainer.DisposeAsync();
        }
    }
}
