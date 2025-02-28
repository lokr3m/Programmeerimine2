using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class _28022025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ShoppingCarts_ShoppingCartId",
                table: "CartProducts");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ShoppingCarts_ShoppingCartId",
                table: "CartProducts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ShoppingCarts_ShoppingCartId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "CartProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ShoppingCarts_ShoppingCartId",
                table: "CartProducts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
