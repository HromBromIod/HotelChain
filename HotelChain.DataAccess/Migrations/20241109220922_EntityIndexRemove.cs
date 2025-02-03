using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelChain.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EntityIndexRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_ExternalId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ExternalId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ExternalId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_HotelRooms_ExternalId",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "HotelRooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "RoomTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Permissions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Hotels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "HotelRooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_ExternalId",
                table: "RoomTypes",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ExternalId",
                table: "Permissions",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ExternalId",
                table: "Hotels",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_ExternalId",
                table: "HotelRooms",
                column: "ExternalId",
                unique: true);
        }
    }
}
