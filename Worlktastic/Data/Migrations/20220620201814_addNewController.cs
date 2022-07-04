using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worlktastic.Data.Migrations
{
    public partial class addNewController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JobPosting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JobPosting");
        }
    }
}
