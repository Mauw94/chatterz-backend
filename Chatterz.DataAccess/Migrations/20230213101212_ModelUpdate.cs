using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatterz.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaceInvadersId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpaceInvaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsGameStarted = table.Column<bool>(type: "bit", nullable: false),
                    IsGameOver = table.Column<bool>(type: "bit", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceInvaders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpaceInvadersId",
                table: "Users",
                column: "SpaceInvadersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SpaceInvaders_SpaceInvadersId",
                table: "Users",
                column: "SpaceInvadersId",
                principalTable: "SpaceInvaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SpaceInvaders_SpaceInvadersId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SpaceInvaders");

            migrationBuilder.DropIndex(
                name: "IX_Users_SpaceInvadersId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpaceInvadersId",
                table: "Users");
        }
    }
}
