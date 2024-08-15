using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlaveryMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectArticul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Articul",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Articul",
                table: "Products",
                column: "Articul",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Articul",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Articul",
                table: "Products",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
