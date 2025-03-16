using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_ICE.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectTaskId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ProjectTaskId",
                table: "Tasks",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "ProjectTaskId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "ProjectTaskId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "ProjectTaskId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTaskId",
                table: "Projects",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "ProjectTaskId");
        }
    }
}
