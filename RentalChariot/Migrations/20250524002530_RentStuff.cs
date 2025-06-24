using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalChariot.Migrations
{
    /// <inheritdoc />
    public partial class RentStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Rents",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Rents");
        }
    }
}
