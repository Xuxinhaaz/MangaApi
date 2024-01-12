using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaApi.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Tags_TagsModelId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_TagsModelId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "TagsModelId",
                table: "Mangas");

            migrationBuilder.AddColumn<string>(
                name: "MangasId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MangasId",
                table: "Tags",
                column: "MangasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Mangas_MangasId",
                table: "Tags",
                column: "MangasId",
                principalTable: "Mangas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Mangas_MangasId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MangasId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MangasId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "TagsModelId",
                table: "Mangas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TagsModelId",
                table: "Mangas",
                column: "TagsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Tags_TagsModelId",
                table: "Mangas",
                column: "TagsModelId",
                principalTable: "Tags",
                principalColumn: "TagsId");
        }
    }
}
