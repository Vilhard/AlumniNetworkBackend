using Microsoft.EntityFrameworkCore.Migrations;

namespace AlumniNetworkBackend.Migrations
{
    public partial class AddedTargetPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetPostId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TargetPostId",
                table: "Posts",
                column: "TargetPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_TargetPostId",
                table: "Posts",
                column: "TargetPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_TargetPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TargetPostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TargetPostId",
                table: "Posts");
        }
    }
}
