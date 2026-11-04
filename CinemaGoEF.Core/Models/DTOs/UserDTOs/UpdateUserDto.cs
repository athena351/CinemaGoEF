namespace CinemaGoEF.Core.Models.DTOs.UserDTOs;

public class UpdateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}