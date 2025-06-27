using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalChariot.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LasLoginTime",
                table: "LoginTokens",
                newName: "LoginTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginTime",
                table: "LoginTokens",
                newName: "LasLoginTime");
        }
    }
}
