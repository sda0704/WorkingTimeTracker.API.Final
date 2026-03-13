using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess.Configuration;

public class TasksConfiguration : IEntityTypeConfiguration<TasksEntity>
{
    public void Configure(EntityTypeBuilder<TasksEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Title)
            .IsRequired();
     

        builder.Property(b => b.IsActive)
            .IsRequired();

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
