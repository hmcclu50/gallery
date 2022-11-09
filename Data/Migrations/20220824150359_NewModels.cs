using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace farris_art_gallery.Data.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Biography = table.Column<string>(type: "TEXT", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbnailName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtworkId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExhibitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fact_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fact_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fact_Exhibit_ExhibitId",
                        column: x => x.ExhibitId,
                        principalTable: "Exhibit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fact_ArtistId",
                table: "Fact",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Fact_ArtworkId",
                table: "Fact",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Fact_ExhibitId",
                table: "Fact",
                column: "ExhibitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fact");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Artwork");
        }
    }
}
