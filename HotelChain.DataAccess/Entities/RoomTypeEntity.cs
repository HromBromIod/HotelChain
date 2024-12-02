using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("RoomTypes")]
public class RoomTypeEntity : IBaseEntity
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    
    public string Type { get; set; }
    public int PricePerDay { get; set; }
    
    public List<HotelRoomEntity> HotelRooms { get; set; }
}