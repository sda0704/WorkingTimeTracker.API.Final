using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess.Configuration;

public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntryEntity>
{
    public void Configure(EntityTypeBuilder<TimeEntryEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.TaskId)
            .IsRequired();
        builder.Property(b => b.Date)
            .IsRequired();

        builder.Property(b => b.Hours)
            .IsRequired();

        builder.Property(b => b.Description)
            .IsRequired();

     
    }
}
