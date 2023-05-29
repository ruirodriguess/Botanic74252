using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuiRumos74252.Migrations
{
    /// <inheritdoc />
    public partial class CartItem22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "CartItems");
        }
    }
}
