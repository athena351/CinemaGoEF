using AutoMapper;
using CinemaGoEF.Application.Services.Interfaces;
using CinemaGoEF.Core.Enums;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.DTOs.BookingDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public BookingService(
        IBookingRepository bookingRepository,
        ISessionRepository sessionRepository,
        ISeatRepository seatRepository,
        ITicketRepository ticketRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _sessionRepository = sessionRepository;
        _seatRepository = seatRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<ICollection<BookingDto>> GetAllBookings()
    {
        ICollection<Booking> bookings = await _bookingRepository.GetAllBooking();

        return _mapper.Map<ICollection<BookingDto>>(bookings);
    }

    public async Task<BookingDto?> GetBookingById(int bookingId)
    {
        Booking? booking = await _bookingRepository.GetBookingById(bookingId);

        if (booking == null)
            return null;

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<ICollection<BookingDto>> GetUserBookings(int userId)
    {
        ICollection<Booking> bookings = await _bookingRepository.GetAllBooking();

        ICollection<Booking> userBookings = bookings
            .Where(b => b.UserId == userId)
            .ToList();

        return _mapper.Map<ICollection<BookingDto>>(userBookings);
    }

    public async Task<ICollection<BookingDto>> GetSessionBookings(int sessionId)
    {
        ICollection<Booking> bookings = await _bookingRepository.GetAllBooking();

        ICollection<Booking> sessionBookings = bookings
            .Where(b => b.SessionId == sessionId)
            .ToList();

        return _mapper.Map<ICollection<BookingDto>>(sessionBookings);
    }

    public async Task<BookingDto> CreateBooking(CreateBookingDto bookingDto)
    {
        if (bookingDto.SeatIds == null || bookingDto.SeatIds.Count == 0)
        {
            throw new InvalidOperationException("You must select at least one seat.");
        }

        if (bookingDto.SeatIds.Distinct().Count() != bookingDto.SeatIds.Count)
        {
            throw new InvalidOperationException("Duplicate seats are not allowed.");
        }

        User user = await _userRepository.GetUserById(bookingDto.UserId);

        Session? session = await _sessionRepository.GetSessionById(bookingDto.SessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        if (session.Status != SESSION_STATUS.Active)
        {
            throw new InvalidOperationException("This session is not active.");
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new InvalidOperationException("This session has already started.");
        }

        List<Seat> selectedSeats = new();

        foreach (int seatId in bookingDto.SeatIds)
        {
            Seat? seat = await _seatRepository.GetById(seatId);

            if (seat == null)
            {
                throw new InvalidOperationException($"Seat {seatId} not found.");
            }

            if (seat.HallId != session.HallId)
            {
                throw new InvalidOperationException("Selected seat does not belong to this hall.");
            }

            selectedSeats.Add(seat);
        }

        ICollection<Booking> allBookings = await _bookingRepository.GetAllBooking();

        foreach (Booking existingBooking in allBookings)
        {
            if (existingBooking.SessionId != session.Id || existingBooking.Status != BOOKING_STATUS.Confirmed)
            {
                continue;
            }

            foreach (Seat selectedSeat in selectedSeats)
            {
                if (existingBooking.Seats.Any(s => s.Id == selectedSeat.Id))
                {
                    throw new InvalidOperationException($"Seat {selectedSeat.Id} is already booked.");
                }
            }
        }

        Booking booking = new Booking
        {
            UserId = user.Id,
            SessionId = session.Id,
            Status = BOOKING_STATUS.Confirmed,
            Seats = selectedSeats
        };

        await _bookingRepository.AddBooking(booking);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task CancelBooking(int userId, int bookingId)
    {
        Booking? booking = await _bookingRepository.GetBookingById(bookingId);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        if (booking.UserId != userId)
        {
            throw new InvalidOperationException("You cannot cancel another user's booking.");
        }

        if (booking.Status == BOOKING_STATUS.Cancelled)
        {
            throw new InvalidOperationException("Booking is already cancelled.");
        }

        Session? session = await _sessionRepository.GetSessionById(booking.SessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new InvalidOperationException("You cannot cancel a booking after the session starts.");
        }

        User user = await _userRepository.GetUserById(userId);

        ICollection<Ticket> tickets = await _ticketRepository.GetAll();

        List<Ticket> bookingTickets = tickets
            .Where(t => t.BookingId == bookingId && !t.IsRefunded)
            .ToList();

        foreach (Ticket ticket in bookingTickets)
        {
            user.Balance += ticket.Price;
            ticket.IsRefunded = true;

            await _ticketRepository.Update(ticket);
        }

        booking.Status = BOOKING_STATUS.Cancelled;

        await _bookingRepository.UpdateBooking(booking);
        await _userRepository.UpdateUser(user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelBookingByAdmin(int bookingId)
    {
        Booking? booking = await _bookingRepository.GetBookingById(bookingId);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        if (booking.Status == BOOKING_STATUS.Cancelled)
        {
            throw new InvalidOperationException("Booking is already cancelled.");
        }

        Session? session = await _sessionRepository.GetSessionById(booking.SessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new InvalidOperationException("This session has already started.");
        }

        booking.Status = BOOKING_STATUS.Cancelled;

        await _bookingRepository.UpdateBooking(booking);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelBookingsForSession(int sessionId)
    {
        ICollection<Booking> bookings = await _bookingRepository.GetAllBooking();

        ICollection<Booking> sessionBookings = bookings
            .Where(b => b.SessionId == sessionId && b.Status == BOOKING_STATUS.Confirmed)
            .ToList();

        ICollection<(string Email, string Username)> emails = new List<(string, string)>();

        ICollection<Ticket> allTickets = await _ticketRepository.GetAll();

        foreach (Booking booking in sessionBookings)
        {
            User user = await _userRepository.GetUserById(booking.UserId);

            List<Ticket> bookingTickets = allTickets
                .Where(t => t.BookingId == booking.Id && !t.IsRefunded)
                .ToList();

            foreach (Ticket ticket in bookingTickets)
            {
                user.Balance += ticket.Price;
                ticket.IsRefunded = true;

                await _ticketRepository.Update(ticket);
            }

            booking.Status = BOOKING_STATUS.Cancelled;

            await _userRepository.UpdateUser(user);
            await _bookingRepository.UpdateBooking(booking);

            emails.Add((user.Email, user.Username));
        }

        await _unitOfWork.SaveChangesAsync();

        foreach (var email in emails)
        {
            await _emailService.SendEmail(
                email.Email,
                "Session Cancelled",
                $"Hello {email.Username}, your session has been cancelled. " +
                "The ticket amount has been refunded to your balance."
            );
        }
    }
}
