namespace HotelChain.BL.Users.Entity;

public class UserModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public int PermissionId { get; set; }
}