namespace WorkingTimeTracker.DataAccess.Entities;

public class TimeEntryEntity
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public DateTime Date {  get; set; }

    public decimal Hours { get; set; }

    public string Description { get; set; } = string.Empty;

    public TasksEntity Task { get; set; }
}
