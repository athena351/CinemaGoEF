using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.MovieDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>();
        CreateMap<MovieDto, Movie>();
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
        CreateMap<Movie, UpdateMovieDto>();
    }
}