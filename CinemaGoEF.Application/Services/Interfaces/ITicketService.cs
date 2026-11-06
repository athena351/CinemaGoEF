using CinemaGoEF.Core.Models.DTOs.TicketDTOs;

namespace CinemaGoEF.Application.Services.Interfaces;

public interface ITicketService
{
    Task<ICollection<TicketDto>> GetAllTickets();

    Task<TicketDto?> GetTicketById(int id);

    Task<ICollection<TicketDto>> PurchaseTickets(int userId, int bookingId, string? promoCode);
}