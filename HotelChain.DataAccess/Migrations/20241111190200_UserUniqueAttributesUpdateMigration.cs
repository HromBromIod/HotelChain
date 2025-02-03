using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelChain.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserUniqueAttributesUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportNumber_PassportSeries",
                table: "Users",
                columns: new[] { "PassportNumber", "PassportSeries" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
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
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PassportNumber_PassportSeries",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
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
    }
}
