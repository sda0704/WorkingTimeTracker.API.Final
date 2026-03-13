
using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly AppDbContext _context;

    public TasksRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tasks>> Get()
    {
        var tasksEntities = await _context.Tasks
            .AsNoTracking()
            .ToListAsync();

        var tasks = tasksEntities
            .Select(b => Tasks.Create(b.Id, b.Title, b.IsActive, b.ProjectId).Tasks)
            .ToList();
        return tasks;
    }


    public async Task<Tasks?> GetById(Guid id)
    {
        var entity = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (entity == null) return null;

        return Tasks.Create(entity.Id, entity.Title, entity.IsActive, entity.ProjectId).Tasks;
    }


    public async Task<Guid> Create(Tasks task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        var projectExist = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
        if (!projectExist)
        {
            throw new InvalidOperationException("Project not found");
        }

        var entity = new TasksEntity
        {
            Id = task.Id,
            Title = task.Title,
            IsActive = task.IsActive,
            ProjectId = task.ProjectId,
        };

        await _context.Tasks.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }


    public async Task<bool> Update(Tasks task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        var rowsAffected = await _context.Tasks
            .Where(t => t.Id == task.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(t => t.Title, task.Title)
            .SetProperty(t => t.IsActive, task.IsActive)
            .SetProperty(t => t.ProjectId, task.ProjectId));

        return rowsAffected > 0;


    }

    public async Task<bool> Delete(Guid id)
    {
        var rowsAffected = await _context.Tasks
             .Where(b => b.Id == id)
             .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }
}
