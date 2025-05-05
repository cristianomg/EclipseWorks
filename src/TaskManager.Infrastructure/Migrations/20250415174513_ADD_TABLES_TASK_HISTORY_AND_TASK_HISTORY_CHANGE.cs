using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TABLES_TASK_HISTORY_AND_TASK_HISTORY_CHANGE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TASK_HISTORY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    UPDATED_BY_USER = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASK_HISTORY_TASK_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TASK",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TASK_HISTORY_CHANGE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoryId = table.Column<int>(type: "integer", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: false),
                    NewValue = table.Column<string>(type: "text", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_HISTORY_CHANGE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASK_HISTORY_CHANGE_TASK_HISTORY_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "TASK_HISTORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4732));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4733));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4734));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 17, 45, 13, 204, DateTimeKind.Utc).AddTicks(4735));

            migrationBuilder.CreateIndex(
                name: "IX_TASK_HISTORY_TaskId",
                table: "TASK_HISTORY",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_HISTORY_CHANGE_HistoryId",
                table: "TASK_HISTORY_CHANGE",
                column: "HistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASK_HISTORY_CHANGE");

            migrationBuilder.DropTable(
                name: "TASK_HISTORY");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4386));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4391));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4393));
        }
    }
}
