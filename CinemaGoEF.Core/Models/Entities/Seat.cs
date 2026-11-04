using System.ComponentModel.DataAnnotations;
using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.Entities;

public class Seat
{
    public int Id { get; set; }
    public int HallId { get; set; }
    public Hall Hall { get; set; }
    [Required]
    [Range(0, 200)]
    public int Row { get; set; }
    [Required]
    [Range(0, 300)]
    public int Number { get; set; }
    public SEAT_TYPE SeatType { get; set; }
    public ICollection<Booking> Bookings { get; set; } =  new List<Booking>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}