using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface IBookingRepository
{
    Task<ICollection<Booking>> GetAllBooking();

    Task<Booking?> GetBookingById(int bookingId);

    Task AddBooking(Booking booking);

    Task UpdateBooking(Booking booking);

    Task DeleteBooking(int bookingId);
}