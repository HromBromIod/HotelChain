using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelChain.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EntityIndexUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
