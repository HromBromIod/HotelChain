using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelChain.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RoomTypeUniqueAttributesUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_Type",
                table: "RoomTypes",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_Type",
                table: "RoomTypes");
        }
    }
}
