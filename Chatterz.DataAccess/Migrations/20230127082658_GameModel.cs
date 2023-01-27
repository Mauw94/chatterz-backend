using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatterz.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WordGuesserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WordGuessers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordToGuess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfGuesses = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsGameStarted = table.Column<bool>(type: "bit", nullable: false),
                    IsGameOver = table.Column<bool>(type: "bit", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    PlayerToStart = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordGuessers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_WordGuesserId",
                table: "Users",
                column: "WordGuesserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WordGuessers_WordGuesserId",
                table: "Users",
                column: "WordGuesserId",
                principalTable: "WordGuessers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WordGuessers_WordGuesserId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "WordGuessers");

            migrationBuilder.DropIndex(
                name: "IX_Users_WordGuesserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WordGuesserId",
                table: "Users");
        }
    }
}
