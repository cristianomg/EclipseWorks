using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EclipseWorks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_SEED_USER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "ID", "CREATED_AT", "NAME", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4386), "Usuario 1", 1 },
                    { 2, new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4391), "Usuario 2", 1 },
                    { 3, new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4392), "Usuario 3", 1 },
                    { 4, new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4393), "Usuario 4", 1 },
                    { 5, new DateTime(2025, 4, 15, 14, 11, 48, 348, DateTimeKind.Utc).AddTicks(4393), "Usuario Gerente 1", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
