using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.SeatDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<Seat, SeatDto>();
        CreateMap<SeatDto, Seat>();
        CreateMap<CreateSeatDto, Seat>();
        CreateMap<Seat, CreateSeatDto>();
        CreateMap<UpdateSeatDto, Seat>();
        CreateMap<Seat, UpdateSeatDto>();
    }
}