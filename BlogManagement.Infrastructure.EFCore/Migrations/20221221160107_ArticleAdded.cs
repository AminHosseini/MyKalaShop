using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ArticleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PictureAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PictureTitle = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CanonicalAddress = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    ArticleCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                        column: x => x.ArticleCategoryId,
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
