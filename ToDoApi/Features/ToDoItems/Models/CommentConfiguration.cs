using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoApi.Features.ToDoItems.Models;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Content).IsRequired().HasMaxLength(1000);
        builder.Property(c => c.CreatedAt).IsRequired();

        builder
            .HasOne(c => c.ToDoItem)
            .WithMany(tdi => tdi.Comments)
            .HasForeignKey(c => c.ToDoItemId)
            .IsRequired();

        builder.ToTable("Comment", "ToDoApp");
    }
}
