using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class RenamedMovietoApiMovieDbMovietoMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListMovies_Movies_DbMovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_DbMovieID",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePeople",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_DbMovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_DbMovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListMovies",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropIndex(
                name: "IX_ListMovies_DbMovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropColumn(
                name: "DbMovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DbMovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "DbMovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "DbMovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "DbMovieID",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "DbMovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                schema: "Movie",
                table: "Movies",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                schema: "Movie",
                table: "MoviePeople",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieID1",
                schema: "Movie",
                table: "MoviePeople",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieID2",
                schema: "Movie",
                table: "MoviePeople",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                schema: "Movie",
                table: "MovieGenres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                schema: "Movie",
                table: "ListMovies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies",
                column: "MovieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePeople",
                schema: "Movie",
                table: "MoviePeople",
                columns: new[] { "MovieID", "PersonID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                schema: "Movie",
                table: "MovieGenres",
                columns: new[] { "MovieID", "GenreID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListMovies",
                schema: "Movie",
                table: "ListMovies",
                columns: new[] { "UserListID", "MovieID" });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePeople_MovieID1",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID1",
                unique: true,
                filter: "[MovieID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePeople_MovieID2",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID2",
                unique: true,
                filter: "[MovieID2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListMovies_MovieID",
                schema: "Movie",
                table: "ListMovies",
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

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID1",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID1",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID2",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID2",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePeople",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_MovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_MovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListMovies",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropIndex(
                name: "IX_ListMovies_MovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.DropColumn(
                name: "MovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "MovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "MovieID2",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropColumn(
                name: "MovieID",
                schema: "Movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "MovieID",
                schema: "Movie",
                table: "ListMovies");

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID",
                schema: "Movie",
                table: "MoviePeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID1",
                schema: "Movie",
                table: "MoviePeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID2",
                schema: "Movie",
                table: "MoviePeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID",
                schema: "Movie",
                table: "MovieGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DbMovieID",
                schema: "Movie",
                table: "ListMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                schema: "Movie",
                table: "Movies",
                column: "DbMovieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePeople",
                schema: "Movie",
                table: "MoviePeople",
                columns: new[] { "DbMovieID", "PersonID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                schema: "Movie",
                table: "MovieGenres",
                columns: new[] { "DbMovieID", "GenreID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListMovies",
                schema: "Movie",
                table: "ListMovies",
                columns: new[] { "UserListID", "DbMovieID" });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePeople_DbMovieID1",
                schema: "Movie",
                table: "MoviePeople",
                column: "DbMovieID1",
                unique: true,
                filter: "[DbMovieID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePeople_DbMovieID2",
                schema: "Movie",
                table: "MoviePeople",
                column: "DbMovieID2",
                unique: true,
                filter: "[DbMovieID2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListMovies_DbMovieID",
                schema: "Movie",
                table: "ListMovies",
                column: "DbMovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListMovies_Movies_DbMovieID",
                schema: "Movie",
                table: "ListMovies",
                column: "DbMovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_DbMovieID",
                schema: "Movie",
                table: "MovieGenres",
                column: "DbMovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID",
                schema: "Movie",
                table: "MoviePeople",
                column: "DbMovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID1",
                schema: "Movie",
                table: "MoviePeople",
                column: "DbMovieID1",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_DbMovieID2",
                schema: "Movie",
                table: "MoviePeople",
                column: "DbMovieID2",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "DbMovieID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
