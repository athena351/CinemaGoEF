namespace CinemaGoEF.Core.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}