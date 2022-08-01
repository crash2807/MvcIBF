using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcIBF.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieVOD");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "MovieId");

            migrationBuilder.CreateTable(
                name: "Movies_VODs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    VODId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies_VODs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_VODs_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_VODs_VODs_VODId",
                        column: x => x.VODId,
                        principalTable: "VODs",
                        principalColumn: "VodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_VODs_MovieId",
                table: "Movies_VODs",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_VODs_VODId",
                table: "Movies_VODs",
                column: "VODId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies_VODs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "MovieId");

            migrationBuilder.CreateTable(
                name: "MovieVOD",
                columns: table => new
                {
                    MoviesMovieId = table.Column<int>(type: "int", nullable: false),
                    VODsVodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieVOD", x => new { x.MoviesMovieId, x.VODsVodId });
                    table.ForeignKey(
                        name: "FK_MovieVOD_Movie_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieVOD_VODs_VODsVodId",
                        column: x => x.VODsVodId,
                        principalTable: "VODs",
                        principalColumn: "VodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieVOD_VODsVodId",
                table: "MovieVOD",
                column: "VODsVodId");
        }
    }
}
