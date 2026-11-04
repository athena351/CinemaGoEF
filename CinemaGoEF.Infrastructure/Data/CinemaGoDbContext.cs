using CinemaGoEF.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaGoEF.Infrastructure.Data;

public class CinemaGoDbContext : DbContext
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<SuperUser> SuperUsers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<VipTicket> VipTickets { get; set; }
    public DbSet<StandardTicket>  StandardTickets { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Session> Sessions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=CinemaGoApp;User Id=sa;Password=MyStrongPassword123!;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.Seats)
            .WithMany(s => s.Bookings)
            .UsingEntity("BookingSeats");
    }
}