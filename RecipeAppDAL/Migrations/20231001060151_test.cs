using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
