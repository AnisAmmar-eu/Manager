using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManager.Migrations
{
    public partial class ChangeNullll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Meetings_MeetingId",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "Templates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Meetings_MeetingId",
                table: "Templates",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Meetings_MeetingId",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Meetings_MeetingId",
                table: "Templates",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
