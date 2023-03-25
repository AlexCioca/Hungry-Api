using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hungry_Api.Migrations
{
    /// <inheritdoc />
    public partial class Addedfollowers7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollower_Users_UserId",
                table: "UserFollower");

            migrationBuilder.DropIndex(
                name: "IX_UserFollower_UserId",
                table: "UserFollower");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFollower");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserFollower",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_UserId",
                table: "UserFollower",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollower_Users_UserId",
                table: "UserFollower",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
