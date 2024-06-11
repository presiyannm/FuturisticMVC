using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futuristic.Data.Migrations
{
    public partial class NewArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_AspNetUsers_UploaderId",
                table: "NewsArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsArticle",
                table: "NewsArticle");

            migrationBuilder.RenameTable(
                name: "NewsArticle",
                newName: "articles");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticle_UploaderId",
                table: "articles",
                newName: "IX_articles_UploaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_articles",
                table: "articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_AspNetUsers_UploaderId",
                table: "articles",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_AspNetUsers_UploaderId",
                table: "articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_articles",
                table: "articles");

            migrationBuilder.RenameTable(
                name: "articles",
                newName: "NewsArticle");

            migrationBuilder.RenameIndex(
                name: "IX_articles_UploaderId",
                table: "NewsArticle",
                newName: "IX_NewsArticle_UploaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsArticle",
                table: "NewsArticle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_AspNetUsers_UploaderId",
                table: "NewsArticle",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
