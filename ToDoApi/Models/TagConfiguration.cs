using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoApi.Models;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{

    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(t => t.ToDoItems)
            .WithMany(t => t.Tags);

        builder.ToTable("Tag", "ToDoApp");
    }
}