using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaGoDbContext _context;
    
    public TicketRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Ticket>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Ticket?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task Update(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int ticketId)
    {
        throw new NotImplementedException();
    }
}