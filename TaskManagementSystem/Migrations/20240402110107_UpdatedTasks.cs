using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_Assignedby",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_Assignedto",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "Assignedto",
                table: "Task",
                newName: "AssignedToUserId");

            migrationBuilder.RenameColumn(
                name: "Assignedby",
                table: "Task",
                newName: "AssignedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_Assignedto",
                table: "Task",
                newName: "IX_Task_AssignedToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_Assignedby",
                table: "Task",
                newName: "IX_Task_AssignedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_AssignedByUserId",
                table: "Task",
                column: "AssignedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_AssignedToUserId",
                table: "Task",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_AssignedByUserId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_AssignedToUserId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "AssignedToUserId",
                table: "Task",
                newName: "Assignedto");

            migrationBuilder.RenameColumn(
                name: "AssignedByUserId",
                table: "Task",
                newName: "Assignedby");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AssignedToUserId",
                table: "Task",
                newName: "IX_Task_Assignedto");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AssignedByUserId",
                table: "Task",
                newName: "IX_Task_Assignedby");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_Assignedby",
                table: "Task",
                column: "Assignedby",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_Assignedto",
                table: "Task",
                column: "Assignedto",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
