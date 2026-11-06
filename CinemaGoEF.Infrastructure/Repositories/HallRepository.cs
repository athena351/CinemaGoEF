using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class HallRepository : IHallRepository
{
    private readonly CinemaGoDbContext _context;

    public HallRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Hall>> GetAll()
    {
        return await _context.Halls.ToListAsync();
    }

    public async Task<Hall?> GetById(int hallId)
    {
        return await _context.Halls.FindAsync(hallId);
    }

    public async Task Add(Hall hall)
    {
        await _context.Halls.AddAsync(hall);
    }

    public async Task Update(Hall hall)
    {
        _context.Halls.Update(hall);
    }

    public async Task Delete(int hallId)
    {
        Hall? hall = await _context.Halls.FindAsync(hallId);

        if (hall != null)
        {
            _context.Halls.Remove(hall);
        }
    }
}