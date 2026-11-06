using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;
using CinemaGoEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly CinemaGoDbContext _context;

    public BookingRepository(CinemaGoDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Booking>> GetAllBooking()
    {
        return await _context.Bookings
            .Include(b => b.Seats)
            .ToListAsync();
    }

    public async Task<Booking?> GetBookingById(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.Seats)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task AddBooking(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task UpdateBooking(Booking booking)
    {
        _context.Bookings.Update(booking);
    }

    public async Task DeleteBooking(int bookingId)
    {
        Booking? booking = await _context.Bookings.FindAsync(bookingId);

        if (booking != null)
        {
            _context.Bookings.Remove(booking);
        }
    }
}