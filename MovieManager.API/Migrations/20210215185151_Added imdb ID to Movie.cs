using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class AddedimdbIDtoMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movie");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "Movie",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "Movie",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Poster = table.Column<string>(nullable: false),
                    imdbID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "Movie",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "UserLists",
                schema: "Movie",
                columns: table => new
                {
                    UserListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLists", x => x.UserListID);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                schema: "Movie",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieID, x.GenreID });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "Movie",
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "Movie",
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoviePeople",
                schema: "Movie",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePeople", x => new { x.MovieID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_MoviePeople_Movies_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "Movie",
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoviePeople_People_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Movie",
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListMovies",
                schema: "Movie",
                columns: table => new
                {
                    UserListID = table.Column<int>(nullable: false),
                    MovieID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListMovies", x => new { x.UserListID, x.MovieID });
                    table.ForeignKey(
                        name: "FK_ListMovies_Movies_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "Movie",
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListMovies_UserLists_UserListID",
                        column: x => x.UserListID,
                        principalSchema: "Movie",
                        principalTable: "UserLists",
                        principalColumn: "UserListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListMovies_MovieID",
                schema: "Movie",
                table: "ListMovies",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreID",
                schema: "Movie",
                table: "MovieGenres",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePeople_PersonID",
                schema: "Movie",
                table: "MoviePeople",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListMovies",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MovieGenres",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "MoviePeople",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "UserLists",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "People",
                schema: "Movie");
        }
    }
}
