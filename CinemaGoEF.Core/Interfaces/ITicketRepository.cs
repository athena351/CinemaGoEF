using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface ITicketRepository
{
    Task<ICollection<Ticket>> GetAll();
    Task<Ticket?> GetById(int id);
    Task Add(Ticket ticket);
    Task Update(Ticket ticket);
    Task Delete(int ticketId);
}