using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hungry_Api.Migrations
{
    /// <inheritdoc />
    public partial class makeuserfollowerscolumnsnotunique3 : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_CurrentUserId",
                table: "UserFollower",
                column: "CurrentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId");
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
    }
}
