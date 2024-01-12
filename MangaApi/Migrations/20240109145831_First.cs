using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaApi.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectionPages",
                columns: table => new
                {
                    CollectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MangaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionPages", x => x.CollectionId);
                });

            migrationBuilder.CreateTable(
                name: "PageModels",
                columns: table => new
                {
                    PageNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageModels", x => x.PageNumber);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MangaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagsId);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagsModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionPageCollectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popularity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mangas_CollectionPages_CollectionPageCollectionId",
                        column: x => x.CollectionPageCollectionId,
                        principalTable: "CollectionPages",
                        principalColumn: "CollectionId");
                    table.ForeignKey(
                        name: "FK_Mangas_Tags_TagsModelId",
                        column: x => x.TagsModelId,
                        principalTable: "Tags",
                        principalColumn: "TagsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_CollectionPageCollectionId",
                table: "Mangas",
                column: "CollectionPageCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_TagsModelId",
                table: "Mangas",
                column: "TagsModelId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropTable(
                name: "PageModels");

            migrationBuilder.DropTable(
                name: "CollectionPages");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
