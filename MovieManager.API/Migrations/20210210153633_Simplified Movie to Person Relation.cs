using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class SimplifiedMovietoPersonRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_MovieID1",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.DropIndex(
                name: "IX_MoviePeople_MovieID2",
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

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople",
                column: "MovieID",
                principalSchema: "Movie",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieID",
                schema: "Movie",
                table: "MoviePeople");

            migrationBuilder.AddColumn<int>(
                name: "MovieID1",
                schema: "Movie",
                table: "MoviePeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieID2",
                schema: "Movie",
                table: "MoviePeople",
                type: "int",
                nullable: true);

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
    }
}
