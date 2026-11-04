using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;

namespace CinemaGoEF.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly CinemaGoDbContext _context;
    
    public BookingRepository(CinemaGoDbContext context)
    {
        _context = context;
    }
    
    public Task<ICollection<Booking>> GetAllBooking()
    {
        throw new NotImplementedException();
    }

    public Task<Booking?> GetBookingById(int bookingId)
    {
        throw new NotImplementedException();
    }

    public Task AddBooking(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBooking(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBooking(int bookingId)
    {
        throw new NotImplementedException();
    }
}