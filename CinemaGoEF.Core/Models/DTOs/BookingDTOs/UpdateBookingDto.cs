using CinemaGoEF.Core.Enums;

namespace CinemaGoEF.Core.Models.DTOs.BookingDTOs;

public class UpdateBookingDto
{
    public BOOKING_STATUS Status { get; set; }
}