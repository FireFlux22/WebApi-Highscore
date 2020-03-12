using Microsoft.EntityFrameworkCore.Migrations;

namespace Highscore.Website.Data.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Game_GameId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_GameId",
                table: "Score");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d057fec1-04ae-415e-a65a-969a6b04d5cd", "9bc89422-d8e6-435e-8a51-264e3756a619", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d057fec1-04ae-415e-a65a-969a6b04d5cd");

            migrationBuilder.CreateIndex(
                name: "IX_Score_GameId",
                table: "Score",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Game_GameId",
                table: "Score",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
