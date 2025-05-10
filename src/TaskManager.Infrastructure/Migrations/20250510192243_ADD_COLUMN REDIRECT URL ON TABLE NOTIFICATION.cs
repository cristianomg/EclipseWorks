using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMNREDIRECTURLONTABLENOTIFICATION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "REDIRECT_URL",
                table: "NOTIFICATION",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 19, 22, 43, 355, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 19, 22, 43, 355, DateTimeKind.Utc).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 19, 22, 43, 355, DateTimeKind.Utc).AddTicks(59));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 19, 22, 43, 355, DateTimeKind.Utc).AddTicks(59));

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5,
                column: "CREATED_AT",
                value: new DateTime(2025, 5, 10, 19, 22, 43, 355, DateTimeKind.Utc).AddTicks(60));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "REDIRECT_URL",
                table: "NOTIFICATION");

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
    }
}
