using CinemaGoEF.Core.Models.DTOs.MovieDTOs;

namespace CinemaGoEF.Application.Services.Interfaces;

public interface IMovieService
{
    Task<ICollection<MovieDto>> GetAllMovies();

    Task<MovieDto?> GetMovieById(int id);

    Task<MovieDto> AddMovie(CreateMovieDto movieDto);

    Task<MovieDto?> UpdateMovie(int id, UpdateMovieDto movieDto);

    Task<bool> DeleteMovie(int id);

    Task<ICollection<MovieDto>> SearchMovies(
        string? searchString,
        string? genre,
        int? maxDuration,
        int? ageRating);
}