using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManager.Migrations
{
    public partial class Chang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Meetings_MeetingId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Projects_ProjectId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_MeetingId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ProjectId",
                table: "Participants");

            migrationBuilder.CreateTable(
                name: "MeetingParticipant",
                columns: table => new
                {
                    MeetingsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingParticipant", x => new { x.MeetingsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_MeetingParticipant_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingParticipant_Participants_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantProject",
                columns: table => new
                {
                    ParticipantsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantProject", x => new { x.ParticipantsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_ParticipantProject_Participants_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipant_ParticipantsId",
                table: "MeetingParticipant",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantProject_ProjectsId",
                table: "ParticipantProject",
                column: "ProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingParticipant");

            migrationBuilder.DropTable(
                name: "ParticipantProject");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_MeetingId",
                table: "Participants",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ProjectId",
                table: "Participants",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Meetings_MeetingId",
                table: "Participants",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Projects_ProjectId",
                table: "Participants",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
