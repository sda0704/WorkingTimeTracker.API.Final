using WorkingTimeTracker.Application.Abstractions;
using WorkingTimeTracker.Application.DTOs;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.DataAccess.Repositories;

namespace WorkingTimeTracker.Application.Services;

public class TimeService : ITimeService
{
    private readonly ITimeEntryRepository _timeEntryService;
    
    private readonly ITasksRepository _taskRepository;

    public TimeService(ITimeEntryRepository timeService, IProjectsRepository projectRepository, ITasksRepository tasksRepository)
    {
        _timeEntryService = timeService;
        _taskRepository = tasksRepository;
    }

    public async Task<List<Time>> GetTimes()
    {

        return await _timeEntryService.Get();
    }
    public async Task<Time?> GetTimesById(Guid timeId)
    {

        return await _timeEntryService.GetById(timeId);
    }

    public async Task<List<Time>> GetTimesByDate(DateTime date)
    {
        return await _timeEntryService.GetByDate(date);
    }
    public async Task<List<Time>> GetTimeByMonth(DateTime month)
    {
        return await _timeEntryService.GetByMonth(month);
    }

    public async Task<Guid> CreateTimes(Time time)
    {
        var TotalHoursForDay = await _timeEntryService.GetTotalHoursForDate(time.Date, null);
        if (TotalHoursForDay + time.Hours > 24)
        {
            throw new InvalidOperationException("Введенное количество часов превышает 24 часа!!");
        }
        var ActiveTask = await _taskRepository.GetById(time.TaskId);
        if (ActiveTask == null) throw new InvalidOperationException("Такой задачи не существует");
        if (ActiveTask.IsActive == false) throw new InvalidOperationException("Задача неактивна!");


        return await _timeEntryService.Create(time);
    }
    public async Task<bool> UpdateTimes(Time time)
    {
        var existingEntry = await _timeEntryService.GetById(time.Id);
        if (existingEntry == null) throw new InvalidOperationException("Проводка не найдена");

        if(existingEntry.TaskId != time.TaskId)
        {
            var oldTask = await _taskRepository.GetById(existingEntry.TaskId);
            if(oldTask != null && !oldTask.IsActive)
            {
                throw new InvalidOperationException("Нельзя сменить задачу так как она неактивна!!");
            }
        }
        var newTask = await _taskRepository.GetById(time.TaskId);
        if (newTask == null) throw new InvalidOperationException("Такой задачи не существует");
        if (!newTask.IsActive) throw new InvalidOperationException("Задача неактивна!!");

        var totalHours = await _timeEntryService.GetTotalHoursForDate(time.Date, time.Id);
        if (totalHours + time.Hours > 24) throw new InvalidOperationException("Превышение лимита 24 часа!");

        return await _timeEntryService.Update(time);
    }
    public async Task<bool> DeleteTimes(Guid timeId)
    {
        return await _timeEntryService.Delete(timeId);
    }

    public async Task<DailySummaryDTO> GetDailySummary (DateTime date)
    {
        var entries = await _timeEntryService.GetByDate(date);
        var totalHours = entries.Sum(e => e.Hours);

        var summary = new DailySummaryDTO
        {
            Date = date,
            TotalHours = totalHours,
        };

        if(totalHours < 8)
        {
            summary.StickerColor = "yellow";
            summary.Message = "Внесено недостаточно часов";
        }
        else if(totalHours == 8)
        {
            summary.StickerColor = "green";
            summary.Message = "Норма выполнена";
        }
        else
        {
            summary.StickerColor = "red";
            summary.Message = "Переработка";
        }

        return summary;

    }
}
