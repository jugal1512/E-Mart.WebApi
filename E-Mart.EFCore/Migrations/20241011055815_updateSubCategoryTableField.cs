using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Mart.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class updateSubCategoryTableField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "SubCategories");
        }
    }
}
