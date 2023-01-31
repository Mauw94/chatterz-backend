using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatterz.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerToStart",
                table: "WordGuessers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerToStart",
                table: "WordGuessers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
