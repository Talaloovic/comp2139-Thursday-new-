using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_ICE.Migrations
{
    /// <inheritdoc />
    public partial class ResyncDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectTaskId",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "ProjectTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectTaskId",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "ProjectTaskId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "ProjectTaskId",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Tasks_ProjectTaskId",
                table: "Projects",
                column: "ProjectTaskId",
                principalTable: "Tasks",
                principalColumn: "ProjectTaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
