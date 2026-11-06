using CinemaGoEF.Core.Enums;
using CinemaGoEF.Core.Interfaces;
using CinemaGoEF.Core.Models.Entities;

namespace CinemaGoEF.Infrastructure.Data;

public class DataInitializer
{
    private readonly IHallRepository _hallRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DataInitializer(IHallRepository hallRepository, ISeatRepository seatRepository, IUnitOfWork unitOfWork)
    {
        _hallRepository = hallRepository;
        _seatRepository = seatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Initialize()
    {
        ICollection<Hall> halls = await _hallRepository.GetAll();

        if (halls.Count == 0)
        {
            Hall hall1 = new Hall
            {
                Name = "Hall 1"
            };

            Hall hall2 = new Hall
            {
                Name = "Hall 2"
            };

            Hall hall3 = new Hall
            {
                Name = "VIP Hall"
            };

            await _hallRepository.Add(hall1);
            await _hallRepository.Add(hall2);
            await _hallRepository.Add(hall3);

            await _unitOfWork.SaveChangesAsync();

            halls = await _hallRepository.GetAll();
        }

        ICollection<Seat> seats = await _seatRepository.GetAll();

        if (seats.Count > 0)
        {
            return;
        }

        foreach (Hall hall in halls)
        {
            await CreateSeats(hall);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task CreateSeats(Hall hall)
    {
        for (int row = 1; row <= 5; row++)
        {
            for (int number = 1; number <= 10; number++)
            {
                Seat seat = new Seat
                {
                    HallId = hall.Id,
                    Row = row,
                    Number = number,
                    SeatType = hall.Name == "VIP Hall"
                        ? SEAT_TYPE.Vip
                        : SEAT_TYPE.Standard
                };

                await _seatRepository.Add(seat);
            }
        }
    }
}