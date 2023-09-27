using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPostcardCollectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postcards_PostcardsImages_ImageId",
                table: "Postcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardsImages",
                table: "PostcardsImages");

            migrationBuilder.RenameTable(
                name: "PostcardsImages",
                newName: "PostcardData");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Postcards",
                newName: "PostcardDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Postcards_ImageId",
                table: "Postcards",
                newName: "IX_Postcards_PostcardDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostcardData",
                table: "PostcardData",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PostcardCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostcardDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostcardCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostcardCollection_PostcardData_PostcardDataId",
                        column: x => x.PostcardDataId,
                        principalTable: "PostcardData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostcardCollection_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostcardCollection_PostcardDataId",
                table: "PostcardCollection",
                column: "PostcardDataId");

            migrationBuilder.CreateIndex(
                name: "IX_PostcardCollection_UserId",
                table: "PostcardCollection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postcards_PostcardData_PostcardDataId",
                table: "Postcards",
                column: "PostcardDataId",
                principalTable: "PostcardData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postcards_PostcardData_PostcardDataId",
                table: "Postcards");

            migrationBuilder.DropTable(
                name: "PostcardCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardData",
                table: "PostcardData");

            migrationBuilder.RenameTable(
                name: "PostcardData",
                newName: "PostcardsImages");

            migrationBuilder.RenameColumn(
                name: "PostcardDataId",
                table: "Postcards",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Postcards_PostcardDataId",
                table: "Postcards",
                newName: "IX_Postcards_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostcardsImages",
                table: "PostcardsImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postcards_PostcardsImages_ImageId",
                table: "Postcards",
                column: "ImageId",
                principalTable: "PostcardsImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
