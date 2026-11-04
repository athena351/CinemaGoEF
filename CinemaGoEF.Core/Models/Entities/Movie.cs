using System.ComponentModel.DataAnnotations;

namespace CinemaGoEF.Core.Models.Entities;

public class Movie
{
    public int Id { get; set; }
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(150)]
    public string Director { get; set; }
    public string Genre { get; set; }
    [Required]
    [Range(0, 500)]
    public int Duration { get; set; }
    [Required]
    [Range(0, 100)]
    public int AgeRating { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}