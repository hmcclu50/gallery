using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace farris_art_gallery.Data.Migrations
{
    public partial class ReworkImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Right",
                table: "Image",
                newName: "WestId");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Image",
                newName: "IsStart");

            migrationBuilder.RenameColumn(
                name: "Left",
                table: "Image",
                newName: "SouthId");

            migrationBuilder.RenameColumn(
                name: "Forward",
                table: "Image",
                newName: "NorthId");

            migrationBuilder.RenameColumn(
                name: "Backward",
                table: "Image",
                newName: "EastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WestId",
                table: "Image",
                newName: "Right");

            migrationBuilder.RenameColumn(
                name: "SouthId",
                table: "Image",
                newName: "Left");

            migrationBuilder.RenameColumn(
                name: "NorthId",
                table: "Image",
                newName: "Forward");

            migrationBuilder.RenameColumn(
                name: "IsStart",
                table: "Image",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "EastId",
                table: "Image",
                newName: "Backward");
        }
    }
}
