using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futuristic.Data.Migrations
{
    public partial class Deleted_Articles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeletedArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsArticleId = table.Column<int>(type: "int", nullable: false),
                    reasonToDelete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedArticles_articles_NewsArticleId",
                        column: x => x.NewsArticleId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeletedArticles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeletedArticles_ApplicationUserId",
                table: "DeletedArticles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedArticles_NewsArticleId",
                table: "DeletedArticles",
                column: "NewsArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedArticles");
        }
    }
}
