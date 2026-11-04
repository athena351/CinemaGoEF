using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class HallRepository : IHallRepository
{
    private readonly CinemaGoDbContext _context;
    
    public HallRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Hall>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Hall?> GetById(int hallId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Hall hall)
    {
        throw new NotImplementedException();
    }

    public Task Update(Hall hall)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int hallId)
    {
        throw new NotImplementedException();
    }
}