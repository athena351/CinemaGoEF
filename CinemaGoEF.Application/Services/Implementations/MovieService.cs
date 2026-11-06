using AutoMapper;
using CinemaGoEF.Application.Services.Interfaces;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.DTOs.MovieDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ICollection<MovieDto>> GetAllMovies()
    {
        ICollection<Movie> movies = await _movieRepository.GetAllMovie();

        return _mapper.Map<ICollection<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetMovieById(int id)
    {
        Movie? movie = await _movieRepository.GetMovieById(id);

        if (movie == null)
        {
            return null;
        }

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> AddMovie(CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);

        await _movieRepository.AddMovie(movie);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto?> UpdateMovie(int id, UpdateMovieDto movieDto)
    {
        Movie? movie = await _movieRepository.GetMovieById(id);

        if (movie == null)
        {
            return null;
        }

        _mapper.Map(movieDto, movie);

        await _movieRepository.UpdateMovie(movie);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<bool> DeleteMovie(int id)
    {
        Movie? movie = await _movieRepository.GetMovieById(id);

        if (movie == null)
        {
            return false;
        }

        await _movieRepository.DeleteMovie(id);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<MovieDto>> SearchMovies(string? searchString, string? genre, int? maxDuration, int? ageRating)
    {
        ICollection<Movie> movies = await _movieRepository.GetAllMovie();

        IEnumerable<Movie> filteredMovies = movies;

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            filteredMovies = filteredMovies.Where(movie =>
                movie.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                movie.Director.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            filteredMovies = filteredMovies.Where(movie => movie.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
        }

        if (maxDuration.HasValue)
        {
            filteredMovies = filteredMovies.Where(movie => movie.Duration <= maxDuration.Value);
        }

        if (ageRating.HasValue)
        {
            filteredMovies = filteredMovies.Where(movie => movie.AgeRating == ageRating.Value);
        }

        return _mapper.Map<ICollection<MovieDto>>(filteredMovies.ToList());
    }
}