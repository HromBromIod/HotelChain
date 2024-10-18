﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelChain.DataAccess.Entities;

[Table("RoomTypes")]
public class RoomTypeEntity
{
    public int Id { get; set; } // PK
    
    public string Type { get; set; }
    public int PricePerDay { get; set; }
    
    public List<HotelRoomEntity> HotelRooms { get; set; }
}