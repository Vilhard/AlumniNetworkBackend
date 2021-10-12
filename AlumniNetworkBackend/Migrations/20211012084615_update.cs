using Microsoft.EntityFrameworkCore.Migrations;

namespace AlumniNetworkBackend.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
