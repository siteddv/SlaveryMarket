using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlaveryMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyAmountToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MoneyAmount",
                table: "AspNetUsers",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyAmount",
                table: "AspNetUsers");
        }
    }
}
