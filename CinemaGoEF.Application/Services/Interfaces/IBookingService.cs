using CinemaGoEF.Core.Models.DTOs.BookingDTOs;

namespace CinemaGoEF.Application.Services.Interfaces;

public interface IBookingService
{
    Task<ICollection<BookingDto>> GetAllBookings();

    Task<BookingDto?> GetBookingById(int bookingId);

    Task<ICollection<BookingDto>> GetUserBookings(int userId);

    Task<ICollection<BookingDto>> GetSessionBookings(int sessionId);

    Task<BookingDto> CreateBooking(CreateBookingDto bookingDto);

    Task CancelBooking(int userId, int bookingId);

    Task CancelBookingByAdmin(int bookingId);

    Task CancelBookingsForSession(int sessionId);
}