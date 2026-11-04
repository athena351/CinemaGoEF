namespace CinemaGoEF.Core.Models.DTOs.SessionDTOs;

public class CreateSessionDto
{
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal StandardPrice { get; set; }
    public decimal VipPrice { get; set; }
}