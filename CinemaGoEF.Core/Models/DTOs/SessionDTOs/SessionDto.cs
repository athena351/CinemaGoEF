using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.SessionDTOs;

public class SessionDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal StandardPrice { get; set; }
    public decimal VipPrice { get; set; }
    public SESSION_STATUS Status { get; set; }
}