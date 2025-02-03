namespace HotelChain.Service.Controllers.Auth.Entities;

public class RegisterUserRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    
    public DateTime? BirthDate { get; set; }
}