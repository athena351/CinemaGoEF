using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.TicketDTOs;

public class TicketDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public int SeatId { get; set; }
    public string TicketCode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TICKET_TYPE TicketType { get; set; }
    public bool IsRefunded { get; set; }
}