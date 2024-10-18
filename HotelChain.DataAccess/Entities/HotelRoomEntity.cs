using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("HotelRooms")]
public class HotelRoomEntity : BaseEntity
{
    public int BedsCount { get; set; }
    public DateTime BookingStartDate { get; set; }
    public DateTime BookingEndDate { get; set; }
    public bool IsBooked { get; set; }
    
    public int HotelId { get; set; }
    public HotelEntity Hotel { get; set; }
    
    public int RoomTypeId { get; set; }
    public RoomTypeEntity RoomType { get; set; }
}