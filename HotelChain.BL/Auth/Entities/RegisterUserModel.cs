namespace HotelChain.BL.Auth.Entities;

public class RegisterUserModel
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
    public DateTime BirthDate { get; set; }
}