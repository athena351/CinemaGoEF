using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaGoDbContext _context;
    
    public MovieRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Movie>> GetAllMovie()
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> GetMovieById(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovie(int id)
    {
        throw new NotImplementedException();
    }
}