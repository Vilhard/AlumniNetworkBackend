using Microsoft.EntityFrameworkCore.Migrations;

namespace AlumniNetworkBackend.Migrations
{
    public partial class removedinvites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventGroupInvite");

            migrationBuilder.DropTable(
                name: "EventUserInvite");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatedById",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventGroup",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroup", x => new { x.EventsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_EventGroup_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.EventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventGroup_GroupsId",
                table: "EventGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UsersId",
                table: "EventUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGroup");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventGroupInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroupInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGroupInvite_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventGroupInvite_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventUserInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUserInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventUserInvite_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventUserInvite_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedById",
                table: "Events",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupInvite_EventId",
                table: "EventGroupInvite",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupInvite_GroupId",
                table: "EventGroupInvite",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserInvite_EventId",
                table: "EventUserInvite",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserInvite_UserId",
                table: "EventUserInvite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
