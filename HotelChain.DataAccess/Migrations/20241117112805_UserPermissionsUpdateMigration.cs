using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelChain.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserPermissionsUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Permissions_PermissionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PermissionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "PermissionEntityUserEntity",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntityUserEntity", x => new { x.PermissionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PermissionEntityUserEntity_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEntityUserEntity_UsersId",
                table: "PermissionEntityUserEntity",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionEntityUserEntity");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionId",
                table: "Users",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Permissions_PermissionId",
                table: "Users",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
