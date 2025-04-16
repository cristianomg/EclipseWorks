using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EclipseWorks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_MAPPING_TASK_COMMENT_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_COMMENT_TASK_TasksId",
                table: "TASK_COMMENT");

            migrationBuilder.DropIndex(
                name: "IX_TASK_COMMENT_TasksId",
                table: "TASK_COMMENT");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "TASK_COMMENT");

            migrationBuilder.AddColumn<int>(
                name: "TASK_ID",
                table: "TASK_COMMENT",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 38, 30, 884, DateTimeKind.Utc).AddTicks(5936));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 38, 30, 884, DateTimeKind.Utc).AddTicks(5942));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 38, 30, 884, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 38, 30, 884, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 38, 30, 884, DateTimeKind.Utc).AddTicks(5944));

            migrationBuilder.CreateIndex(
                name: "IX_TASK_COMMENT_TASK_ID",
                table: "TASK_COMMENT",
                column: "TASK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_COMMENT_TASK_TASK_ID",
                table: "TASK_COMMENT",
                column: "TASK_ID",
                principalTable: "TASK",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_COMMENT_TASK_TASK_ID",
                table: "TASK_COMMENT");

            migrationBuilder.DropIndex(
                name: "IX_TASK_COMMENT_TASK_ID",
                table: "TASK_COMMENT");

            migrationBuilder.DropColumn(
                name: "TASK_ID",
                table: "TASK_COMMENT");

            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "TASK_COMMENT",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 10, 4, 402, DateTimeKind.Utc).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 10, 4, 402, DateTimeKind.Utc).AddTicks(6589));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 10, 4, 402, DateTimeKind.Utc).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 10, 4, 402, DateTimeKind.Utc).AddTicks(6591));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 16, 14, 10, 4, 402, DateTimeKind.Utc).AddTicks(6591));

            migrationBuilder.CreateIndex(
                name: "IX_TASK_COMMENT_TasksId",
                table: "TASK_COMMENT",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_COMMENT_TASK_TasksId",
                table: "TASK_COMMENT",
                column: "TasksId",
                principalTable: "TASK",
                principalColumn: "ID");
        }
    }
}
