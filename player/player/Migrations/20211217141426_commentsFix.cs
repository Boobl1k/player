using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace player.Migrations
{
    public partial class commentsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Comments");
        }
    }
}
