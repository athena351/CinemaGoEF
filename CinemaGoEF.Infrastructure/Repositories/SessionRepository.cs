using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaGoDbContext _context;

    public SessionRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Session>> GetAllSessions()
    {
        return await _context.Sessions.ToListAsync();
    }

    public async Task<Session?> GetSessionById(int sessionId)
    {
        return await _context.Sessions.FindAsync(sessionId);
    }

    public async Task AddSession(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }

    public async Task UpdateSession(Session session)
    {
        _context.Sessions.Update(session);
    }

    public async Task DeleteSession(int sessionId)
    {
        Session? session = await _context.Sessions.FindAsync(sessionId);

        if (session != null)
        {
            _context.Sessions.Remove(session);
        }
    }
}