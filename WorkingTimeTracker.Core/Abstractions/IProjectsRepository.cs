using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.DataAccess.Repositories
{
    public interface IProjectsRepository
    {
        Task<Guid> Create(Project project);
        Task<Guid> Delete(Guid id);
        Task<List<Project>> Get();
        Task<Guid?> Update(Guid id, string title, string code, bool isActive);
    }
}