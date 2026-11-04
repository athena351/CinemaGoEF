using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.BookingDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(
                dest => dest.SeatIds,
                opt => opt.MapFrom(src => src.Seats.Select(s => s.Id))
            );

        CreateMap<BookingDto, Booking>()
            .ForMember(
                dest => dest.Seats,
                opt => opt.Ignore()
            );

        CreateMap<CreateBookingDto, Booking>()
            .ForMember(
                dest => dest.Seats,
                opt => opt.Ignore()
            );

        CreateMap<UpdateBookingDto, Booking>();

        CreateMap<Booking, UpdateBookingDto>();
    }
}