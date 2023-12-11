using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScarletScreen.Migrations
{
    /// <inheritdoc />
    public partial class Addtmdbid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tmdbuserid",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tmdbuserid",
                table: "users");
        }
    }
}
