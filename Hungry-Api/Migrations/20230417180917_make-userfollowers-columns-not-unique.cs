using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hungry_Api.Migrations
{
    /// <inheritdoc />
    public partial class makeuserfollowerscolumnsnotunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollower_CurrentUserId",
                table: "UserFollower");

            migrationBuilder.DropIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower");

            migrationBuilder.AlterColumn<int>(
                name: "FollowerId",
                table: "UserFollower",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentUserId",
                table: "UserFollower",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_CurrentUserId",
                table: "UserFollower",
                column: "CurrentUserId",
                unique: true,
                filter: "[CurrentUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId",
                unique: true,
                filter: "[FollowerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollower_CurrentUserId",
                table: "UserFollower");

            migrationBuilder.DropIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower");

            migrationBuilder.AlterColumn<int>(
                name: "FollowerId",
                table: "UserFollower",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentUserId",
                table: "UserFollower",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_CurrentUserId",
                table: "UserFollower",
                column: "CurrentUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId",
                unique: true);
        }
    }
}
