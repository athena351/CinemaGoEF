using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface ISeatRepository
{
    Task<ICollection<Seat>> GetAll();
    Task<Seat?> GetById(int seatId);
    Task Add(Seat seat);
    Task Update(Seat seat);
    Task Delete(int seatId);
}