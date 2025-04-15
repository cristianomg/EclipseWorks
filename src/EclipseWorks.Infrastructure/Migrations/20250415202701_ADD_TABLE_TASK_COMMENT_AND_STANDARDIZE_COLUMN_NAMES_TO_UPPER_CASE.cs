using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EclipseWorks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TABLE_TASK_COMMENT_AND_STANDARDIZE_COLUMN_NAMES_TO_UPPER_CASE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_PROJECT_ProjectId",
                table: "TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_TASK_HISTORY_TASK_TaskId",
                table: "TASK_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_TASK_HISTORY_CHANGE_TASK_HISTORY_HistoryId",
                table: "TASK_HISTORY_CHANGE");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "USER",
                newName: "ROLE");

            migrationBuilder.RenameColumn(
                name: "Field",
                table: "TASK_HISTORY_CHANGE",
                newName: "FIELD");

            migrationBuilder.RenameColumn(
                name: "OldValue",
                table: "TASK_HISTORY_CHANGE",
                newName: "OLD_VALUE");

            migrationBuilder.RenameColumn(
                name: "NewValue",
                table: "TASK_HISTORY_CHANGE",
                newName: "NEW_VALUE");

            migrationBuilder.RenameColumn(
                name: "HistoryId",
                table: "TASK_HISTORY_CHANGE",
                newName: "TASK_HISTORY_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_HISTORY_CHANGE_HistoryId",
                table: "TASK_HISTORY_CHANGE",
                newName: "IX_TASK_HISTORY_CHANGE_TASK_HISTORY_ID");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "TASK_HISTORY",
                newName: "TASK_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_HISTORY_TaskId",
                table: "TASK_HISTORY",
                newName: "IX_TASK_HISTORY_TASK_ID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "TASK",
                newName: "PROJECT_ID");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "TASK",
                newName: "DUE_DATE");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_ProjectId",
                table: "TASK",
                newName: "IX_TASK_PROJECT_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "USER",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "ROLE",
                table: "USER",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK_HISTORY_CHANGE",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "FIELD",
                table: "TASK_HISTORY_CHANGE",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OLD_VALUE",
                table: "TASK_HISTORY_CHANGE",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK_HISTORY",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "PROJECT",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.CreateTable(
                name: "TASK_COMMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VALUE = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    TasksId = table.Column<int>(type: "integer", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_COMMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASK_COMMENT_TASK_TasksId",
                        column: x => x.TasksId,
                        principalTable: "TASK",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_AT", "ROLE", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7433), "User", null });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CREATED_AT", "ROLE", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7435), "User", null });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CREATED_AT", "ROLE", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7436), "User", null });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CREATED_AT", "ROLE", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7437), "User", null });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CREATED_AT", "ROLE", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7438), "Manager", null });

            migrationBuilder.CreateIndex(
                name: "IX_TASK_COMMENT_TasksId",
                table: "TASK_COMMENT",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_PROJECT_PROJECT_ID",
                table: "TASK",
                column: "PROJECT_ID",
                principalTable: "PROJECT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_HISTORY_TASK_TASK_ID",
                table: "TASK_HISTORY",
                column: "TASK_ID",
                principalTable: "TASK",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_HISTORY_CHANGE_TASK_HISTORY_TASK_HISTORY_ID",
                table: "TASK_HISTORY_CHANGE",
                column: "TASK_HISTORY_ID",
                principalTable: "TASK_HISTORY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_PROJECT_PROJECT_ID",
                table: "TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_TASK_HISTORY_TASK_TASK_ID",
                table: "TASK_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_TASK_HISTORY_CHANGE_TASK_HISTORY_TASK_HISTORY_ID",
                table: "TASK_HISTORY_CHANGE");

            migrationBuilder.DropTable(
                name: "TASK_COMMENT");

            migrationBuilder.RenameColumn(
                name: "ROLE",
                table: "USER",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "FIELD",
                table: "TASK_HISTORY_CHANGE",
                newName: "Field");

            migrationBuilder.RenameColumn(
                name: "TASK_HISTORY_ID",
                table: "TASK_HISTORY_CHANGE",
                newName: "HistoryId");

            migrationBuilder.RenameColumn(
                name: "OLD_VALUE",
                table: "TASK_HISTORY_CHANGE",
                newName: "OldValue");

            migrationBuilder.RenameColumn(
                name: "NEW_VALUE",
                table: "TASK_HISTORY_CHANGE",
                newName: "NewValue");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_HISTORY_CHANGE_TASK_HISTORY_ID",
                table: "TASK_HISTORY_CHANGE",
                newName: "IX_TASK_HISTORY_CHANGE_HistoryId");

            migrationBuilder.RenameColumn(
                name: "TASK_ID",
                table: "TASK_HISTORY",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_HISTORY_TASK_ID",
                table: "TASK_HISTORY",
                newName: "IX_TASK_HISTORY_TaskId");

            migrationBuilder.RenameColumn(
                name: "PROJECT_ID",
                table: "TASK",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "DUE_DATE",
                table: "TASK",
                newName: "DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_TASK_PROJECT_ID",
                table: "TASK",
                newName: "IX_TASK_ProjectId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "USER",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "USER",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK_HISTORY_CHANGE",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Field",
                table: "TASK_HISTORY_CHANGE",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "TASK_HISTORY_CHANGE",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK_HISTORY",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "TASK",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_AT",
                table: "PROJECT",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_AT", "Role", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4730), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CREATED_AT", "Role", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4732), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CREATED_AT", "Role", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4733), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CREATED_AT", "Role", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4734), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CREATED_AT", "Role", "UPDATED_AT" },
                values: new object[] { new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4735), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_PROJECT_ProjectId",
                table: "TASK",
                column: "ProjectId",
                principalTable: "PROJECT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_HISTORY_TASK_TaskId",
                table: "TASK_HISTORY",
                column: "TaskId",
                principalTable: "TASK",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_HISTORY_CHANGE_TASK_HISTORY_HistoryId",
                table: "TASK_HISTORY_CHANGE",
                column: "HistoryId",
                principalTable: "TASK_HISTORY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
