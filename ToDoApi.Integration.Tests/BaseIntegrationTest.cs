namespace ToDoApi.Integration.Tests;

[SetUpFixture]
public class BaseIntegrationTest
{
    public static SqlServerContainerFixture SqlServerFixture { get; private set; }

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        SqlServerFixture = new SqlServerContainerFixture();
        await SqlServerFixture.StartAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await SqlServerFixture.StopAsync();
    }
}
