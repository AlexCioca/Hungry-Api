using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hungry_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Users",
                newName: "Password");
        }
    }
}
