namespace HotelChain.Service.Controllers.Users.Entities;

public class UserFilter
{
    public string? LoginPart { get; set; }
    public string? NamePart { get; set; }
    public string? PhoneNumberPart { get; set; }
    public string? EmailPart { get; set; }
    
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    public int? Permission { get; set; }
}