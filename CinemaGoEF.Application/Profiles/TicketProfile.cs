using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.TicketDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketDto>();

        CreateMap<StandardTicket, TicketDto>();
        CreateMap<VipTicket, TicketDto>();

        CreateMap<CreateTicketDto, StandardTicket>()
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.IsRefunded, opt => opt.Ignore());

        CreateMap<CreateTicketDto, VipTicket>()
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.IsRefunded, opt => opt.Ignore());

        CreateMap<TicketDto, StandardTicket>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<TicketDto, VipTicket>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateTicketDto, StandardTicket>();

        CreateMap<UpdateTicketDto, VipTicket>();

        CreateMap<Ticket, UpdateTicketDto>();
    }
}