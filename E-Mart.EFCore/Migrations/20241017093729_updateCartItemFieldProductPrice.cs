using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Mart.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class updateCartItemFieldProductPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductPrice",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "CartItems");
        }
    }
}
