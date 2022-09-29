using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcIBF.Migrations
{
    public partial class MovieStarFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie_Stars_Functions",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    StarId = table.Column<int>(type: "int", nullable: false),
                    FunctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_Stars_Functions", x => new { x.MovieId, x.StarId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_Movie_Stars_Functions_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "FunctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movie_Stars_Functions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movie_Stars_Functions_Stars_StarId",
                        column: x => x.StarId,
                        principalTable: "Stars",
                        principalColumn: "StarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Stars_Functions_FunctionId",
                table: "Movie_Stars_Functions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Stars_Functions_StarId",
                table: "Movie_Stars_Functions",
                column: "StarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_Stars_Functions");
        }
    }
}
