using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Data.Migrations
{
    /// <inheritdoc />
    public partial class taosanpham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaPham_TheLoai_TheLoaiId",
                table: "SaPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaPham",
                table: "SaPham");

            migrationBuilder.RenameTable(
                name: "SaPham",
                newName: "SanPham");

            migrationBuilder.RenameIndex(
                name: "IX_SaPham_TheLoaiId",
                table: "SanPham",
                newName: "IX_SanPham_TheLoaiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_TheLoai_TheLoaiId",
                table: "SanPham",
                column: "TheLoaiId",
                principalTable: "TheLoai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_TheLoai_TheLoaiId",
                table: "SanPham");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPham",
                table: "SanPham");

            migrationBuilder.RenameTable(
                name: "SanPham",
                newName: "SaPham");

            migrationBuilder.RenameIndex(
                name: "IX_SanPham_TheLoaiId",
                table: "SaPham",
                newName: "IX_SaPham_TheLoaiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaPham",
                table: "SaPham",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaPham_TheLoai_TheLoaiId",
                table: "SaPham",
                column: "TheLoaiId",
                principalTable: "TheLoai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
