    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcIBF.Migrations
{
    public partial class CompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies_VODs",
                table: "Movies_VODs");

            migrationBuilder.DropIndex(
                name: "IX_Movies_VODs_MovieId",
                table: "Movies_VODs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Movies_VODs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies_VODs",
                table: "Movies_VODs",
                columns: new[] { "MovieId", "VODId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies_VODs",
                table: "Movies_VODs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Movies_VODs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies_VODs",
                table: "Movies_VODs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_VODs_MovieId",
                table: "Movies_VODs",
                column: "MovieId");
        }
    }
}
