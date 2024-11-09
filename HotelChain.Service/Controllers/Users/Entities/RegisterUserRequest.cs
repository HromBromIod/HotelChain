using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelChain.Service.Controllers.Users.Entities;

public class RegisterUserRequest
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public int PermissionId { get; set; }
}