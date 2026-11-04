using AutoMapper;
using CinemaGoEF.Core.Models.DTOs.SessionDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Profiles;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<Session, SessionDto>();
        CreateMap<SessionDto, Session>();
        CreateMap<CreateSessionDto, Session>();
        CreateMap<Session, CreateSessionDto>();
        CreateMap<UpdateSessionDto, Session>();
        CreateMap<Session, UpdateSessionDto>();
    }
}