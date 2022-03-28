using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIG2Server.Migrations
{
    public partial class addIP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "SoftwareOMs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "SoftwareOMs");
        }
    }
}
