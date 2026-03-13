using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess.Configuration;

public class ProjectsConfiguration : IEntityTypeConfiguration<ProjectsEntity>
{
    public void Configure(EntityTypeBuilder<ProjectsEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Title)
            .IsRequired();
        builder.Property(b => b.Code)
            .IsRequired();

        builder.Property(b => b.IsActive)
            .IsRequired();
        
    }
}
