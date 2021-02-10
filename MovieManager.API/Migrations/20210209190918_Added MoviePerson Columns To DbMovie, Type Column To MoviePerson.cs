using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class AddedMoviePersonColumnsToDbMovieTypeColumnToMoviePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_People_DirectorID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_People_WriterPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_WriterPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "DirectorMovieID",
                schema: "Movie",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectorPersonID",
                schema: "Movie",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                schema: "Movie",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WriterMovieID",
                schema: "Movie",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Movie",
                table: "MoviePeople",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorMovieID_DirectorPersonID",
                schema: "Movie",
                table: "Movies",
                columns: new[] { "DirectorMovieID", "DirectorPersonID" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_WriterMovieID_WriterPersonID",
                schema: "Movie",
                table: "Movies",
                columns: new[] { "WriterMovieID", "WriterPersonID" });

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
                name: "FK_Movies_MoviePeople_DirectorMovieID_DirectorPersonID",
                schema: "Movie",
                table: "Movies",
                columns: new[] { "DirectorMovieID", "DirectorPersonID" },
                principalSchema: "Movie",
                principalTable: "MoviePeople",
                principalColumns: new[] { "MovieID", "PersonID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MoviePeople_WriterMovieID_WriterPersonID",
                schema: "Movie",
                table: "Movies",
                columns: new[] { "WriterMovieID", "WriterPersonID" },
                principalSchema: "Movie",
                principalTable: "MoviePeople",
                principalColumns: new[] { "MovieID", "PersonID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MoviePeople_DirectorMovieID_DirectorPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MoviePeople_WriterMovieID_WriterPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectorMovieID_DirectorPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_WriterMovieID_WriterPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectorMovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectorPersonID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WriterID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WriterMovieID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorID",
                schema: "Movie",
                table: "Movies",
                column: "DirectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_WriterPersonID",
                schema: "Movie",
                table: "Movies",
                column: "WriterPersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_People_DirectorID",
                schema: "Movie",
                table: "Movies",
                column: "DirectorID",
                principalSchema: "Movie",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_People_WriterPersonID",
                schema: "Movie",
                table: "Movies",
                column: "WriterPersonID",
                principalSchema: "Movie",
                principalTable: "People",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
