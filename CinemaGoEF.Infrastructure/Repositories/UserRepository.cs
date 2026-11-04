using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CinemaGoDbContext _context;
    
    public UserRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}