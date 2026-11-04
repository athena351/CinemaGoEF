using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.Entities;

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public BOOKING_STATUS Status { get; set; }
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}