using HotelChain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.DataAccess;

public class HotelChainDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<HotelEntity> Hotels { get; set; }
    public DbSet<HotelRoomEntity> HotelRooms { get; set; }
    public DbSet<RoomTypeEntity> RoomTypes { get; set; }

    public HotelChainDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
        modelBuilder.Entity<UserEntity>().HasOne(u => u.Permission)
            .WithMany(p => p.Users)
            .HasForeignKey(u => u.PermissionId);

        modelBuilder.Entity<PermissionEntity>().HasKey(p => p.Id);

        modelBuilder.Entity<HotelEntity>().HasKey(h => h.Id);

        modelBuilder.Entity<HotelRoomEntity>().HasKey(r => r.Id);
        modelBuilder.Entity<HotelRoomEntity>().HasOne(r => r.RoomType)
            .WithMany(t => t.HotelRooms)
            .HasForeignKey(r => r.RoomTypeId);
        modelBuilder.Entity<HotelRoomEntity>().HasOne(r => r.Hotel)
            .WithMany(h => h.HotelRooms)
            .HasForeignKey(r => r.HotelId);

        modelBuilder.Entity<RoomTypeEntity>().HasKey(t => t.Id);
    }
}