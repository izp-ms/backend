using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePostcards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostcardImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostcardImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(280)", maxLength: 280, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postcards_PostcardImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "PostcardImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPostcard",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostcardId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPostcard", x => new { x.UserId, x.PostcardId });
                    table.ForeignKey(
                        name: "FK_UserPostcard_Postcards_PostcardId",
                        column: x => x.PostcardId,
                        principalTable: "Postcards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPostcard_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postcards_ImageId",
                table: "Postcards",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPostcard_PostcardId",
                table: "UserPostcard",
                column: "PostcardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPostcard");

            migrationBuilder.DropTable(
                name: "Postcards");

            migrationBuilder.DropTable(
                name: "PostcardImage");
        }
    }
}
