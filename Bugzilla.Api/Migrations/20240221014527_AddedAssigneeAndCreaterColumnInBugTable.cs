using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bugzilla.Api.Migrations
{
    public partial class AddedAssigneeAndCreaterColumnInBugTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "Bug",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreaterId",
                table: "Bug",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_AssigneeId",
                table: "Bug",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bug_CreaterId",
                table: "Bug",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_AspNetUsers_AssigneeId",
                table: "Bug",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bug_AspNetUsers_CreaterId",
                table: "Bug",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bug_AspNetUsers_AssigneeId",
                table: "Bug");

            migrationBuilder.DropForeignKey(
                name: "FK_Bug_AspNetUsers_CreaterId",
                table: "Bug");

            migrationBuilder.DropIndex(
                name: "IX_Bug_AssigneeId",
                table: "Bug");

            migrationBuilder.DropIndex(
                name: "IX_Bug_CreaterId",
                table: "Bug");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Bug");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Bug");
        }
    }
}
