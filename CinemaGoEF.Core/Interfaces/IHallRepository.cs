using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Core.Interfaces;

public interface IHallRepository
{
    Task<ICollection<Hall>> GetAll();
    Task<Hall?> GetById(int hallId);
    Task Add(Hall hall);
    Task Update(Hall hall);
    Task Delete(int hallId);
}