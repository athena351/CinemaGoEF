using System.ComponentModel.DataAnnotations;

namespace CinemaGoEF.Core.Models.Entities;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    [Required]
    [MaxLength(200)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MaxLength(300)]
    public string Password { get; set; }
    [Required]
    public decimal Balance { get; set; } = 0;

    public ICollection<Booking> Bookings { get; set; } = new  List<Booking>();
}