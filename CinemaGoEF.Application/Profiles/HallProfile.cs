using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.HallDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class HallProfile : Profile
{
    public HallProfile()
    {
        CreateMap<Hall, HallDto>();
        CreateMap<HallDto, Hall>();
        CreateMap<CreateHallDto, Hall>();
        CreateMap<Hall, CreateHallDto>();
        CreateMap<UpdateHallDto, Hall>();
        CreateMap<Hall, UpdateHallDto>();
    }
}