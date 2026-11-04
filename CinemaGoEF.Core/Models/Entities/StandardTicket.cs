namespace CinemaGoEF.Core.Models.Entities;

public class StandardTicket : Ticket
{
    public override decimal CalculatePrice()
    {
        return Price;
    }
}