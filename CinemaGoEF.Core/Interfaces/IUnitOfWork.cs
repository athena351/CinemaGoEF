namespace CinemaGoEF.Core.Interfaces;

public interface IUnitOfWork
{
    int SaveChanges();
}