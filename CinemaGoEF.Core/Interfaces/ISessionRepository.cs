using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface ISessionRepository
{
    Task<ICollection<Session>> GetAllSessions();
    Task<Session?> GetSessionById(int id);
    Task AddSession(Session session);
    Task UpdateSession(Session session);
    Task DeleteSession(int id);
}