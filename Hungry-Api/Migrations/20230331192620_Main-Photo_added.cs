using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hungry_Api.Migrations
{
    /// <inheritdoc />
    public partial class MainPhoto_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Recipes");
        }
    }
}
