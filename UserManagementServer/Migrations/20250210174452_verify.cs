using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementServer.Migrations
{
    public partial class verify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 17, 44, 51, 454, DateTimeKind.Utc).AddTicks(5282), new DateTime(2025, 2, 10, 17, 44, 51, 454, DateTimeKind.Utc).AddTicks(5933) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 17, 44, 51, 457, DateTimeKind.Utc).AddTicks(2519), new DateTime(2025, 2, 10, 17, 44, 51, 457, DateTimeKind.Utc).AddTicks(3493) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 10, 17, 44, 51, 457, DateTimeKind.Utc).AddTicks(4201), new DateTime(2025, 2, 10, 17, 44, 51, 457, DateTimeKind.Utc).AddTicks(4202) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 13, 47, 55, 60, DateTimeKind.Utc).AddTicks(4876), new DateTime(2025, 2, 9, 13, 47, 55, 60, DateTimeKind.Utc).AddTicks(5207) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(8582), new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(8927) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(9241) });
        }
    }
}
