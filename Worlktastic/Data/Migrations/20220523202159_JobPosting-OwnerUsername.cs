using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worlktastic.Data.Migrations
{
    public partial class JobPostingOwnerUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "JobPosting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "JobPosting");
        }
    }
}
