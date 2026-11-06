using CinemaGoEF.Core.Models.DTOs.SessionDTOs;

namespace CinemaGoEF.Application.Services.Interfaces;

public interface ISessionService
{
    Task<ICollection<SessionDto>> GetAllSessions();

    Task<SessionDto?> GetSessionById(int id);

    Task<ICollection<SessionDto>> FilterSessions(int? movieId, DateTime? date);

    Task<SessionDto> CreateSession(CreateSessionDto sessionDto);

    Task<SessionDto?> UpdateSession(int id, UpdateSessionDto sessionDto);

    Task<bool> CancelSession(int id);
}