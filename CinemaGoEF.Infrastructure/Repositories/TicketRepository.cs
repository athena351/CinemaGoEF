using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaGoDbContext _context;

    public TicketRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Ticket>> GetAll()
    {
        return await _context.Set<Ticket>().ToListAsync();
    }

    public async Task<Ticket?> GetById(int ticketId)
    {
        return await _context.Set<Ticket>().FindAsync(ticketId);
    }

    public async Task Add(Ticket ticket)
    {
        await _context.Set<Ticket>().AddAsync(ticket);
    }

    public async Task Update(Ticket ticket)
    {
        _context.Set<Ticket>().Update(ticket);
    }

    public async Task Delete(int ticketId)
    {
        Ticket? ticket = await _context.Set<Ticket>().FindAsync(ticketId);

        if (ticket != null)
        {
            _context.Set<Ticket>().Remove(ticket);
        }
    }
}