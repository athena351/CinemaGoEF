using System.ComponentModel.DataAnnotations;

namespace CinemaGoEF.Core.Models.Entities;

public class Hall
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}