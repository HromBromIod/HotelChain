using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("Hotels")]
public class HotelEntity : BaseEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Adress { get; set; }
    
    public List<HotelRoomEntity> HotelRooms { get; set; }
}