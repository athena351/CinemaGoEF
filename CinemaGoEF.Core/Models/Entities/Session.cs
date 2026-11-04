using System.ComponentModel.DataAnnotations;
using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.Entities;

public class Session
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int HallId { get; set; }
    public Hall Hall { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public decimal StandardPrice { get; set; }
    [Required]
    public decimal VipPrice { get; set; }
    public SESSION_STATUS Status { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}