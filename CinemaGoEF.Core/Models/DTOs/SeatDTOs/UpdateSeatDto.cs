using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.SeatDTOs;

public class UpdateSeatDto
{
    public int Row { get; set; }
    public int Number { get; set; }
    public SEAT_TYPE SeatType { get; set; }
}