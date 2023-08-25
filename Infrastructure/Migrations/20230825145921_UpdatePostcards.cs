using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostcards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postcards_PostcardImage_ImageId",
                table: "Postcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardImage",
                table: "PostcardImage");

            migrationBuilder.RenameTable(
                name: "PostcardImage",
                newName: "PostcardsImages");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postcards_PostcardsImages_ImageId",
                table: "Postcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardsImages",
                table: "PostcardsImages");

            migrationBuilder.RenameTable(
                name: "PostcardsImages",
                newName: "PostcardImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostcardImage",
                table: "PostcardImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postcards_PostcardImage_ImageId",
                table: "Postcards",
                column: "ImageId",
                principalTable: "PostcardImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
