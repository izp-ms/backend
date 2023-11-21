using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveTypeToProperModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Postcards");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PostcardData",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "PLACE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PostcardData");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Postcards",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
