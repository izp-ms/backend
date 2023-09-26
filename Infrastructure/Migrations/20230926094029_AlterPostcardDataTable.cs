using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterPostcardDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserPostcards",
                newName: "ReceivedAt");

            migrationBuilder.AddColumn<int>(
                name: "CollectRangeInMeters",
                table: "PostcardsImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectRangeInMeters",
                table: "PostcardsImages");

            migrationBuilder.RenameColumn(
                name: "ReceivedAt",
                table: "UserPostcards",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Address",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);
        }
    }
}
