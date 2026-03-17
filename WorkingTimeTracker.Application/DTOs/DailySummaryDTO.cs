namespace WorkingTimeTracker.Application.DTOs;

public class DailySummaryDTO
{
    public DateTime Date { get; set; }
    public decimal TotalHours { get; set; }
    public string StickerColor { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
