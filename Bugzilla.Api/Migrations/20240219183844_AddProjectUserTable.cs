using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bugzilla.Api.Migrations
{
    public partial class AddProjectUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Project_User",
                 columns: table => new
                 {
                     Id = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     ProjectId = table.Column<int>(type: "int", nullable: false),
                     UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Project_User", x => x.Id);
                     table.ForeignKey(
                         name: "FK_Project_User_AspNetUsers_UserId",
                         column: x => x.UserId,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_Project_User_Project_ProjectId",
                         column: x => x.ProjectId,
                         principalTable: "Project",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                 });

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_ProjectId",
                table: "Project_User",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_UserId",
                table: "Project_User",
                column: "UserId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                     name: "Project_User");


        }

    }

  
}
