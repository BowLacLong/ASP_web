using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Data.Migrations
{
    /// <inheritdoc />
    public partial class xoaSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "SanPham");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "SanPham",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SanPham",
                newName: "price");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
