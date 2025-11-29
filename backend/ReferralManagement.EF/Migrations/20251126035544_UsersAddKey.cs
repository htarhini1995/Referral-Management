using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManagement.EF.Migrations
{
    /// <inheritdoc />
    public partial class UsersAddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MobilePhoneNumber",
                schema: "public",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                schema: "public",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "public",
                table: "UserLogin",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "public",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "public",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "public",
                table: "UserLogin");

            migrationBuilder.DropIndex(
                name: "IX_UserLogin_UserId",
                schema: "public",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "MobilePhoneNumber",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "UserLogin");
        }
    }
}
