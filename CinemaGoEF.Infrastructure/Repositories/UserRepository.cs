using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CinemaGoDbContext _context;

    public UserRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<User>> GetAllUsers()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Set<User>().FindAsync(userId);
    }

    public async Task AddUser(User user)
    {
        await _context.Set<User>().AddAsync(user);
    }

    public async Task UpdateUser(User user)
    {
        _context.Set<User>().Update(user);
    }

    public async Task DeleteUser(int userId)
    {
        User? user = await _context.Set<User>().FindAsync(userId);

        if (user != null)
        {
            _context.Set<User>().Remove(user);
        }
    }
}