using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HotelChain.DataAccess.Entities;

[Table("Users")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public List<PermissionEntity> Permissions { get; set; }
}

public class UserRoleEntity : IdentityRole<int>
{}