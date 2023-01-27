using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatterz.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GameConnectionIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameConnectionId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameConnectionId",
                table: "Users");
        }
    }
}
