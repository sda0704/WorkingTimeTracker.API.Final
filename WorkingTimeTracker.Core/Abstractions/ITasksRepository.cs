using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.DataAccess.Repositories
{
    public interface ITasksRepository
    {
        Task<Guid> Create(Tasks task);
        Task<bool> Delete(Guid id);
        Task<List<Tasks>> Get();
        Task<Tasks?> GetById(Guid id);
        Task<bool> Update(Tasks task);
    }
}