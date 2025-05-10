using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_NOTIFICATION_TABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NOTIFICATION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VALUE = table.Column<string>(type: "VARCHAR(1000)", nullable: false),
                    USER_ID = table.Column<int>(type: "integer", nullable: false),
                    READ = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    READ_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_USER_ID",
                table: "NOTIFICATION",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTIFICATION");

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
        }
    }
}
