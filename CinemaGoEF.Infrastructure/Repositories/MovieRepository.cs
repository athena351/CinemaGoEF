using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaGoDbContext _context;

    public MovieRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Movie>> GetAllMovie()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie?> GetMovieById(int movieId)
    {
        return await _context.Movies.FindAsync(movieId);
    }

    public async Task AddMovie(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
    }

    public async Task UpdateMovie(Movie movie)
    {
        _context.Movies.Update(movie);
    }

    public async Task DeleteMovie(int movieId)
    {
        Movie? movie = await _context.Movies.FindAsync(movieId);

        if (movie != null)
        {
            _context.Movies.Remove(movie);
        }
    }
}