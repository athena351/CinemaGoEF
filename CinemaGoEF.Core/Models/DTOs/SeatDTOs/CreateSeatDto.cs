using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.SeatDTOs;

public class CreateSeatDto
{
    public int HallId { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public SEAT_TYPE SeatType { get; set; }
}