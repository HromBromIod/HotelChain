using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("Permissions")]
public class PermissionEntity
{
    public int Id { get; set; } // PK
    
    public string Type { get; set; }
    
    public List<UserEntity> Users { get; set; }
}