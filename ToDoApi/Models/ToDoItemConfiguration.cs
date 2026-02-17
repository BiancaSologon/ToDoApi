using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoApi.Models;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(500).IsRequired();
        builder.Property(r => r.IsComplete).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();

        builder.HasOne(tdi => tdi.ToDoDetails)
               .WithOne(tdd => tdd.ToDoItem)
               .HasForeignKey<ToDoDetails>(tdd => tdd.ToDoItemId);

        builder.HasMany(tdi => tdi.Comments)
               .WithOne(c => c.ToDoItem)
               .HasForeignKey(c => c.ToDoItemId)
               .IsRequired();

        builder.HasMany(tdi => tdi.Tags)
            .WithMany(t => t.ToDoItems)
            .UsingEntity("ToDoItemTag",
            j => j.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId").HasPrincipalKey(nameof(Tag.Id)),
            j => j.HasOne(typeof(ToDoItem)).WithMany().HasForeignKey("ToDoItemId").HasPrincipalKey(nameof(ToDoItem.Id)),
            j => j.HasKey("TagId", "ToDoItemId"));

        builder.ToTable("ToDoItem", "ToDoApp");
    }
}
