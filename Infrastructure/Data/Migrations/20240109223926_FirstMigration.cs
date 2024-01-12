using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "TagsModelId",
                table: "Mangas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Tags_TagsModelId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_TagsModelId",
                table: "Mangas");

            migrationBuilder.AlterColumn<string>(
                name: "TagsModelId",
                table: "Mangas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TagsModelId",
                table: "Mangas",
                column: "TagsModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Tags_TagsModelId",
                table: "Mangas",
                column: "TagsModelId",
                principalTable: "Tags",
                principalColumn: "TagsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
