namespace CinemaGoEF.Core.Models.Entities;

public class VipTicket : Ticket
{
    public override decimal CalculatePrice()
    {
        return Price;
    }
}