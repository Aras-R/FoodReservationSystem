using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodReservation.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Foods");
        }
    }
}
