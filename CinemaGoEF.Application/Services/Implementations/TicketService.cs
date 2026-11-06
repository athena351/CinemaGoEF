using AutoMapper;
using CinemaGoEF.Application.Services.Interfaces;
using CinemaGoEF.Core.Enums;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.DTOs.TicketDTOs;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Application.Services.Implementations;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public TicketService(
        ITicketRepository ticketRepository,
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        ISessionRepository sessionRepository,
        ISeatRepository seatRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEmailService emailService)
    {
        _ticketRepository = ticketRepository;
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _seatRepository = seatRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<ICollection<TicketDto>> GetAllTickets()
    {
        ICollection<Ticket> tickets = await _ticketRepository.GetAll();

        return _mapper.Map<ICollection<TicketDto>>(tickets);
    }

    public async Task<TicketDto?> GetTicketById(int id)
    {
        Ticket? ticket = await _ticketRepository.GetById(id);

        if (ticket == null)
            return null;

        return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<ICollection<TicketDto>> PurchaseTickets(int userId, int bookingId, string? promoCode)
    {
        Booking? booking = await _bookingRepository.GetBookingById(bookingId);

        if (booking == null)
        {
            throw new InvalidOperationException("Booking not found.");
        }

        if (booking.UserId != userId)
        {
            throw new InvalidOperationException("This booking does not belong to this user.");
        }

        if (booking.Status != BOOKING_STATUS.Confirmed)
        {
            throw new InvalidOperationException("Booking is not confirmed.");
        }
        
        ICollection<Ticket> allTickets = await _ticketRepository.GetAll();

        bool alreadyPurchased = allTickets.Any(t => t.BookingId == bookingId);

        if (alreadyPurchased)
        {
            throw new InvalidOperationException("Tickets have already been purchased for this booking.");
        }
        
        Session? session = await _sessionRepository.GetSessionById(booking.SessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        if (session.Status != SESSION_STATUS.Active)
        {
            throw new InvalidOperationException("Session is not active.");
        }

        if (session.StartTime <= DateTime.Now)
        {
            throw new InvalidOperationException("Session has already started.");
        }
        
        User user = await _userRepository.GetUserById(userId);
        
        if (booking.Seats.Count == 0)
        {
            throw new InvalidOperationException("Booking does not contain any seats.");
        }

        decimal totalPrice = 0;

        List<Ticket> newTickets = new();

        foreach (Seat bookingSeat in booking.Seats)
        {
            Seat? seat = await _seatRepository.GetById(bookingSeat.Id);

            if (seat == null)
            {
                throw new InvalidOperationException($"Seat {bookingSeat.Id} not found.");
            }

            if (seat.HallId != session.HallId)
            {
                throw new InvalidOperationException("Seat does not belong to the session hall.");
            }

            Ticket ticket;

            if (seat.SeatType == SEAT_TYPE.Vip)
            {
                ticket = new VipTicket
                {
                    TicketType = TICKET_TYPE.Vip,
                    Price = session.VipPrice
                };
            }
            else
            {
                ticket = new StandardTicket
                {
                    TicketType = TICKET_TYPE.Standard,
                    Price = session.StandardPrice
                };
            }

            ticket.BookingId = bookingId;
            ticket.SeatId = seat.Id;
            ticket.TicketCode = GenerateTicketCode();
            ticket.IsRefunded = false;

            totalPrice += ticket.CalculatePrice();

            newTickets.Add(ticket);
        }

        if (promoCode != null && promoCode.Trim().ToUpper() == "CINEMA26")
        {
            totalPrice = totalPrice * 0.80m;
        }

        if (user.Balance < totalPrice)
        {
            throw new InvalidOperationException("Insufficient balance.");
        }

        user.Balance -= totalPrice;

        await _userRepository.UpdateUser(user);

        foreach (Ticket ticket in newTickets)
        {
            await _ticketRepository.Add(ticket);
        }

        await _unitOfWork.SaveChangesAsync();

        string emailBody = "Your tickets have been purchased successfully.\n\n";

        foreach (Ticket ticket in newTickets)
        {
            emailBody +=
                $"Ticket code: {ticket.TicketCode}\n" +
                $"Seat ID: {ticket.SeatId}\n" +
                $"Price: {ticket.Price}\n\n";
        }

        emailBody += $"Total paid: {totalPrice}";

        await _emailService.SendEmail(user.Email, "Your CinemaGo Tickets", emailBody);

        return _mapper.Map<ICollection<TicketDto>>(newTickets);
    }

    private string GenerateTicketCode()
    {
        return Guid.NewGuid()
            .ToString("N")
            .Substring(0, 8)
            .ToUpper();
    }
}
