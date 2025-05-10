using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMNDELAYEDONTABLETASK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DELAYED",
                table: "TASK",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 15, 23, 19, 789, DateTimeKind.Utc).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 15, 23, 19, 789, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 15, 23, 19, 789, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 15, 23, 19, 789, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 15, 23, 19, 789, DateTimeKind.Utc).AddTicks(7890));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DELAYED",
                table: "TASK");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 12, 6, 52, 72, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 12, 6, 52, 72, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 12, 6, 52, 72, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 12, 6, 52, 72, DateTimeKind.Utc).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 12, 6, 52, 72, DateTimeKind.Utc).AddTicks(4183));
        }
    }
}
