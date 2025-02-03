using HotelChain.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
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
        //специальные таблицы для IdentityServer
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
        modelBuilder.Entity<UserRoleEntity>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
        
        modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.ExternalId).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.UserName).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(u => new { u.PassportNumber, u.PassportSeries }).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.PhoneNumber).IsUnique();
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<UserEntity>().HasMany(u => u.Permissions) 
            .WithMany(p => p.Users);

        modelBuilder.Entity<PermissionEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<PermissionEntity>().HasIndex(p => p.ExternalId).IsUnique();
        modelBuilder.Entity<PermissionEntity>().HasIndex(p => p.Type).IsUnique();

        modelBuilder.Entity<HotelEntity>().HasKey(h => h.Id);
        modelBuilder.Entity<HotelEntity>().HasIndex(h => h.ExternalId).IsUnique();
        modelBuilder.Entity<HotelEntity>().HasIndex(h => h.Name).IsUnique();

        modelBuilder.Entity<HotelRoomEntity>().HasKey(r => r.Id);
        modelBuilder.Entity<HotelRoomEntity>().HasIndex(r => r.ExternalId).IsUnique();
        modelBuilder.Entity<HotelRoomEntity>().HasOne(r => r.RoomType)
            .WithMany(t => t.HotelRooms)
            .HasForeignKey(r => r.RoomTypeId);
        modelBuilder.Entity<HotelRoomEntity>().HasOne(r => r.Hotel)
            .WithMany(h => h.HotelRooms)
            .HasForeignKey(r => r.HotelId);

        modelBuilder.Entity<RoomTypeEntity>().HasKey(t => t.Id);
        modelBuilder.Entity<RoomTypeEntity>().HasIndex(t => t.ExternalId).IsUnique();
        modelBuilder.Entity<RoomTypeEntity>().HasIndex(t => t.Type).IsUnique();
    }
}