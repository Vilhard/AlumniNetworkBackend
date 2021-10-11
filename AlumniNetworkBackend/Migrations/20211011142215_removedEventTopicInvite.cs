using Microsoft.EntityFrameworkCore.Migrations;

namespace AlumniNetworkBackend.Migrations
{
    public partial class removedEventTopicInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTopicInvite");

            migrationBuilder.CreateTable(
                name: "EventTopic",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTopic", x => new { x.EventsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_EventTopic_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTopic_TopicsId",
                table: "EventTopic",
                column: "TopicsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTopic");

            migrationBuilder.CreateTable(
                name: "EventTopicInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTopicInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTopicInvite_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventTopicInvite_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTopicInvite_EventId",
                table: "EventTopicInvite",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTopicInvite_TopicId",
                table: "EventTopicInvite",
                column: "TopicId");
        }
    }
}
