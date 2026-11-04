namespace CinemaGoEF.Core.Models.DTOs.MovieDTOs;

public class UpdateMovieDto
{
    public string Title { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public int AgeRating { get; set; }
}