namespace WorkingTimeTracker.DataAccess.Entities;

public class TasksEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public bool IsActive { get; set; }

    public Guid ProjectId { get; set; }
    public ProjectsEntity Project { get; set; }
}
