using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.DataAccess.Entities;




namespace WorkingTimeTracker.DataAccess.Repositories;


public class ProjectsRepository : IProjectsRepository
{
    private readonly AppDbContext _context;

    public ProjectsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> Get()
    {
        var projectEntities = await _context.Projects
            .AsNoTracking()
            .ToListAsync();

        var projects = projectEntities
            .Select(b => Project.Create(b.Id, b.Title, b.Code, b.IsActive).Project)
            .ToList();
        return projects;
    }

    public async Task<Guid> Create(Project project)
    {
        if (project == null) throw new ArgumentNullException(nameof(project));
        var projectEntity = new ProjectsEntity
        {
            Id = project.Id,
            Title = project.Title,
            Code = project.Code,
            IsActive = project.IsActive,
        };

        await _context.Projects.AddAsync(projectEntity);
        await _context.SaveChangesAsync();

        return projectEntity.Id;
    }

    public async Task<Guid?> Update(Guid id, string title, string code, bool isActive)
    {
        var exists = await _context.Projects.AnyAsync(p => p.Id == id);

        if (!exists)
            return null;

        await _context.Projects
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(b => b.Title, b => title)
            .SetProperty(b => b.Code, b => code)
            .SetProperty(b => b.IsActive, b => isActive));


        return id;
    }


    public async Task<Guid> Delete(Guid id)
    {
        await _context.Projects
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return id;

    }



}
