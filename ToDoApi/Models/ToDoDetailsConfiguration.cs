using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoApi.Models;

public class ToDoDetailsConfiguration : IEntityTypeConfiguration<ToDoDetails>
{
    public void Configure(EntityTypeBuilder<ToDoDetails> builder)
    {
        builder.HasKey("Id");
        builder.Property(tdd => tdd.Notes).HasMaxLength(1000);
        builder.Property(tdd => tdd.Location).HasMaxLength(200);
        builder.Property(tdd => tdd.EstimatedMinutes).IsRequired();

        builder.HasOne(tdd => tdd.ToDoItem)
               .WithOne(tdi => tdi.ToDoDetails)
               .HasForeignKey<ToDoDetails>(tdd => tdd.ToDoItemId);

        builder.ToTable("ToDoDetails", "ToDoApp");
    }
}
