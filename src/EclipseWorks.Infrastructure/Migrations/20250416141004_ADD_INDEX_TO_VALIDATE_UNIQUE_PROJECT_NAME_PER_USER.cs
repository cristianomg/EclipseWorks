using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EclipseWorks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_INDEX_TO_VALIDATE_UNIQUE_PROJECT_NAME_PER_USER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_PROJECT_NAME_USER_ID",
                table: "PROJECT",
                columns: new[] { "NAME", "USER_ID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PROJECT_NAME_USER_ID",
                table: "PROJECT");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7435));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7437));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 4, 15, 20, 27, 1, 30, DateTimeKind.Utc).AddTicks(7438));
        }
    }
}
