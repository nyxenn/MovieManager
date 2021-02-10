using Microsoft.EntityFrameworkCore.Migrations;

namespace MMApi.Migrations
{
    public partial class DbMovieTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectorID",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WriterID",
                schema: "Movie",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectorID",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
