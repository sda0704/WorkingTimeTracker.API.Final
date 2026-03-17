using WorkingTimeTracker.Application.DTOs;
using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.Application.Abstractions
{
    public interface ITimeService
    {
        Task<Guid> CreateTimes(Time time);
        Task<bool> DeleteTimes(Guid timeId);
        Task<List<Time>> GetTimes();
        Task<List<Time>> GetTimesByDate(DateTime date);
        Task<List<Time>> GetTimeByMonth(DateTime month);
        Task<Time?> GetTimesById(Guid timeId);
        Task<bool> UpdateTimes(Time time);
        Task<DailySummaryDTO> GetDailySummary(DateTime date);
    }
}