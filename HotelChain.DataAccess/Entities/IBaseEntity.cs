namespace HotelChain.DataAccess.Entities;

public interface IBaseEntity
{
    public int Id { get; set; } // PK
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
} 