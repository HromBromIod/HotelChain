using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("Permissions")]
public class PermissionEntity : BaseEntity
{
    public string Type { get; set; }
    
    public List<UserEntity> Users { get; set; }
}