using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatterz.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedScoreToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "WordGuessers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "WordGuessers");
        }
    }
}
