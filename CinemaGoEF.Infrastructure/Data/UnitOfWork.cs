using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CinemaGoDbContext _context;

    public UnitOfWork(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}