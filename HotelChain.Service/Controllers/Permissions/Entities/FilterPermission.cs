namespace HotelChain.Service.Controllers.Permissions.Entities;

public class FilterPermission
{
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public List<string>? TypeParts { get; set; }
}