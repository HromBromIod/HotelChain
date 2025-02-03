namespace HotelChain.BL.Permissions.Entity;

public class PermissionModel
{
    public int Id { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    
    public string Type { get; set; }
}