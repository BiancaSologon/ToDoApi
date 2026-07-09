using AwesomeAssertions;
using ToDoApi.Features.Common;
using ToDoApi.Features.ToDoItems.Commands;
using ToDoApi.Features.ToDoItems.Models;

namespace ToDoApi.Integration.Tests.ToDoItems;

[TestFixture]
public class ToDoItemDeleteCommandHandlerTests : BaseIntegrationTest
{
    private ToDoContext _toDoContext;
    private ToDoItemDeleteCommandHandler _sut = null!;

    [Test]
    public async Task Handle_Should_delete_item_When_item_exists()
    {
        var toDoItem = new ToDoItem
        {
            Name = "Test ToDo Item",
            Description = "This is a test to-do item.",
            IsComplete = false,
            CreatedAt = DateTime.UtcNow,
        };
        _toDoContext.ToDoItems.Add(toDoItem);
        await _toDoContext.SaveChangesAsync();

        var command = new ToDoItemDeleteCommand { Id = toDoItem.Id };
        var result = await _sut.Handle(command, CancellationToken.None);

        Assert.That(result.IsSuccess, Is.True);

        result.IsSuccess.Should().BeTrue();
        _toDoContext.ToDoItems.Find(toDoItem.Id).Should().BeNull();
    }

    [OneTimeSetUp]
    public async Task Setup()
    {
        _toDoContext = await CreateNewDbContext();
        _sut = new ToDoItemDeleteCommandHandler(_toDoContext);
    }

    [OneTimeTearDown]
    public async Task Teardown()
    {
        await _toDoContext.DisposeAsync();
    }
}
