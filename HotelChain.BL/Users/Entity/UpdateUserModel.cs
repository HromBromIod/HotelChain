namespace HotelChain.BL.Users.Entity;

public class UpdateUserModel
{
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    
    public int? PassportSeries { get; set; }
    public int? PassportNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    public string? FullName { get; set; }
    public DateTime? BirthDate { get; set; }
}