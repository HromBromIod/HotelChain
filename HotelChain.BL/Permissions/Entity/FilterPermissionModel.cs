namespace HotelChain.BL.Permissions.Entity;

public class FilterPermissionModel
{
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public List<string>? Types { get; set; }
}