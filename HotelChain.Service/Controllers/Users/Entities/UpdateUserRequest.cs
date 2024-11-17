namespace HotelChain.Service.Controllers.Users.Entities;

public class UpdateUserRequest
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
    
    public int? PassportSeries { get; set; }
    public int? PassportNumber { get; set; }
    
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
}