using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace farris_art_gallery.Data.Migrations
{
    public partial class Directions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Backward",
                table: "Image",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Forward",
                table: "Image",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "Image",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Right",
                table: "Image",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backward",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Forward",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "Right",
                table: "Image");
        }
    }
}
