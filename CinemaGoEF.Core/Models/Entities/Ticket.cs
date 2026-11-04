using System.ComponentModel.DataAnnotations;
using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.Entities;

public abstract class Ticket
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    [Required]
    [MaxLength(200)]
    public string TicketCode { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }
    public TICKET_TYPE TicketType { get; set; }
    public bool IsRefunded { get; set; }

    public abstract decimal CalculatePrice();
}