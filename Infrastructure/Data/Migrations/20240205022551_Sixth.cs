using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaApi.Migrations
{
    /// <inheritdoc />
    public partial class Sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_CollectionPages_CollectionPageCollectionId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_CollectionPageCollectionId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "CollectionPageCollectionId",
                table: "Mangas");

            migrationBuilder.AddColumn<string>(
                name: "ChapterName",
                table: "CollectionPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MangasId",
                table: "CollectionPages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "chapterSummary",
                table: "CollectionPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPages_MangasId",
                table: "CollectionPages",
                column: "MangasId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionPages_Mangas_MangasId",
                table: "CollectionPages",
                column: "MangasId",
                principalTable: "Mangas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionPages_Mangas_MangasId",
                table: "CollectionPages");

            migrationBuilder.DropIndex(
                name: "IX_CollectionPages_MangasId",
                table: "CollectionPages");

            migrationBuilder.DropColumn(
                name: "ChapterName",
                table: "CollectionPages");

            migrationBuilder.DropColumn(
                name: "MangasId",
                table: "CollectionPages");

            migrationBuilder.DropColumn(
                name: "chapterSummary",
                table: "CollectionPages");

            migrationBuilder.AddColumn<string>(
                name: "CollectionPageCollectionId",
                table: "Mangas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_CollectionPageCollectionId",
                table: "Mangas",
                column: "CollectionPageCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_CollectionPages_CollectionPageCollectionId",
                table: "Mangas",
                column: "CollectionPageCollectionId",
                principalTable: "CollectionPages",
                principalColumn: "CollectionId");
        }
    }
}
