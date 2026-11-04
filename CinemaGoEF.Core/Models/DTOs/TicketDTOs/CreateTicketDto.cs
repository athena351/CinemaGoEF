namespace CinemaGoEF.Core.Models.DTOs.TicketDTOs;

public class CreateTicketDto
{
    public int BookingId { get; set; }
    public int SeatId { get; set; }
    public string TicketCode { get; set; } = string.Empty;
}