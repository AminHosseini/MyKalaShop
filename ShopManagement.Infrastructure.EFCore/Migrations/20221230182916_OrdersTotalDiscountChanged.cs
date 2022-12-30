using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTotalDiscountChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "TotalDiscountAmount",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscountAmount",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "TotalDiscount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
