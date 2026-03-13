
namespace WorkingTimeTracker.Core.Models;

public class Project
{
    private Project(Guid id, string title, string code, bool IsActive)
    {
        Id = id; Title = title; Code = code; this.IsActive = IsActive;
    }
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public bool IsActive { get; private set; }

    public static (Project Project, string error) Create(Guid id, string title, string code, bool isActive)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(code))
        {
            error = "Название или код не могут быть пустыми";
        }
        var project = new Project(id, title, code, isActive);
        return (project, error);
    }
}
