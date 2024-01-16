using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaApi.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MangaUrl",
                table: "PageModels");

            migrationBuilder.AddColumn<string>(
                name: "CollectionPageCollectionId",
                table: "PageModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersProfiles",
                columns: table => new
                {
                    UsersProfileModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserBio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserHasReadMangas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfiles", x => x.UsersProfileModelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersProfileModelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserRoles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UsersProfiles_UsersProfileModelId",
                        column: x => x.UsersProfileModelId,
                        principalTable: "UsersProfiles",
                        principalColumn: "UsersProfileModelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageModels_CollectionPageCollectionId",
                table: "PageModels",
                column: "CollectionPageCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UsersProfileModelId",
                table: "Users",
                column: "UsersProfileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageModels_CollectionPages_CollectionPageCollectionId",
                table: "PageModels",
                column: "CollectionPageCollectionId",
                principalTable: "CollectionPages",
                principalColumn: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageModels_CollectionPages_CollectionPageCollectionId",
                table: "PageModels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PageModels_CollectionPageCollectionId",
                table: "PageModels");

            migrationBuilder.DropColumn(
                name: "CollectionPageCollectionId",
                table: "PageModels");

            migrationBuilder.AddColumn<string>(
                name: "MangaUrl",
                table: "PageModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
