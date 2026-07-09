using Microsoft.EntityFrameworkCore;
using ToDoApi.Features.Common;

namespace ToDoApi.Integration.Tests;

[SetUpFixture]
public class BaseIntegrationTest
{
    private static SqlServerContainerFixture SqlServerFixture { get; set; } =
        new SqlServerContainerFixture();

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        await SqlServerFixture.StartAsync();
    }

    protected static async Task<ToDoContext> CreateNewDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseSqlServer(SqlServerFixture.DbContainer!.GetConnectionString())
            .Options;

        var dbContext = new ToDoContext(options);
        await dbContext.Database.MigrateAsync();

        return dbContext;
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await SqlServerFixture.StopAsync();
    }
}
