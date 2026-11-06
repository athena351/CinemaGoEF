using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly CinemaGoDbContext _context;

    public SeatRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Seat>> GetAll()
    {
        return await _context.Seats.ToListAsync();
    }

    public async Task<Seat?> GetById(int seatId)
    {
        return await _context.Seats.FindAsync(seatId);
    }

    public async Task Add(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
    }

    public async Task Update(Seat seat)
    {
        _context.Seats.Update(seat);
    }

    public async Task Delete(int seatId)
    {
        Seat? seat = await _context.Seats.FindAsync(seatId);

        if (seat != null)
        {
            _context.Seats.Remove(seat);
        }
    }
}