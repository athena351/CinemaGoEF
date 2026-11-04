using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.BookingDTOs;

public class BookingDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
    public BOOKING_STATUS Status { get; set; }
    public List<int> SeatIds { get; set; } = new();
}