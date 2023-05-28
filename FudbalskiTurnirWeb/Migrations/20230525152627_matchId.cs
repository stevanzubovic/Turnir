using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FudbalskiTurnirWeb.Migrations
{
    public partial class matchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_AwayTeamId",
                table: "Match",
                column: "AwayTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_AwayTeamId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Match");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                columns: new[] { "AwayTeamId", "HomeTeamId" });
        }
    }
}
