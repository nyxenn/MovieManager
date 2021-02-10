using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class RenamedMovieIDToDbMovieID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListMovies_Movies_MovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MovieID",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID",
                schema: "Movie",
                table: "Movies",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies",
                column: "DbMovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListMovies_Movies_MovieID",
                schema: "Movie",
                table: "ListMovies",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MovieID",
                schema: "Movie",
                table: "MovieGenres",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListMovies_Movies_MovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_MovieID",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DbMovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListMovies_Movies_MovieID",
                schema: "Movie",
                table: "ListMovies",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_MovieID",
                schema: "Movie",
                table: "MovieGenres",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
