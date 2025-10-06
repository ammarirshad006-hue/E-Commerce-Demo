using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Migrations
{
    /// <inheritdoc />
    public partial class FixProductStockRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_Products_Id",
                table: "stocks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "stocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_ProductId",
                table: "stocks",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_Products_ProductId",
                table: "stocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_Products_ProductId",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "IX_stocks_ProductId",
                table: "stocks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "stocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_Products_Id",
                table: "stocks",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
