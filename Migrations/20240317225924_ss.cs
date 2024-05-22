using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_AlbumId1",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_AlbumId1",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "AlbumId1",
                table: "Song");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlbumId1",
                table: "Song",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId1",
                table: "Song",
                column: "AlbumId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_AlbumId1",
                table: "Song",
                column: "AlbumId1",
                principalTable: "Album",
                principalColumn: "Id");
        }
    }
}
