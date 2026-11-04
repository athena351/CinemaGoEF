using CinemaGoEF.Core.Interfaces;

namespace CinemaGoEF.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CinemaGoDbContext _context;
    
    public UnitOfWork(CinemaGoDbContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}