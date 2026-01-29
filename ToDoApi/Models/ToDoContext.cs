using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
