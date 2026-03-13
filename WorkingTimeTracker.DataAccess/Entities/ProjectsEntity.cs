namespace WorkingTimeTracker.DataAccess.Entities;

public class ProjectsEntity
{

    public Guid Id { get; set; }    
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; }
  
    public List<TasksEntity> Tasks { get; set; } = new List<TasksEntity>();
        

}
