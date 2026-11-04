using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaGoDbContext _context;
    
    public SessionRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Session>> GetAllSessions()
    {
        throw new NotImplementedException();
    }

    public Task<Session?> GetSessionById(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddSession(Session session)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSession(Session session)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSession(int id)
    {
        throw new NotImplementedException();
    }
}