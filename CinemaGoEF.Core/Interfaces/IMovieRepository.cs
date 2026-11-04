using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface IMovieRepository
{
    Task<ICollection<Movie>> GetAllMovie();
    Task<Movie?> GetMovieById(int id);
    Task AddMovie(Movie movie);
    Task UpdateMovie(Movie movie);
    Task DeleteMovie(int id);
}