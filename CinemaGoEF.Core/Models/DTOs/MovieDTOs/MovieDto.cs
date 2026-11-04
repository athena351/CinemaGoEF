namespace CinemaGoEF.Core.Models.DTOs.MovieDTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public int AgeRating { get; set; }
}