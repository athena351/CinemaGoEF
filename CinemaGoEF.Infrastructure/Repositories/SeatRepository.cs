using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly CinemaGoDbContext _context;

    public SeatRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Seat>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Seat?> GetById(int seatId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Seat seat)
    {
        throw new NotImplementedException();
    }

    public Task Update(Seat seat)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int seatId)
    {
        throw new NotImplementedException();
    }
}