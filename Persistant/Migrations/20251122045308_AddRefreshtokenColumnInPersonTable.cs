using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistant.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshtokenColumnInPersonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Persons");
        }
    }
}
