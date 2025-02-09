using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementServer.Migrations
{
    public partial class SeedStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusID", "CreatedAt", "ModifiedAt", "StatusName" },
                values: new object[] { 2, new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 2, 9, 13, 47, 55, 61, DateTimeKind.Utc).AddTicks(9241), "Deactive" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 8, 15, 8, 3, 985, DateTimeKind.Utc).AddTicks(1859), new DateTime(2025, 2, 8, 15, 8, 3, 985, DateTimeKind.Utc).AddTicks(2176) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 8, 15, 8, 3, 986, DateTimeKind.Utc).AddTicks(7407), new DateTime(2025, 2, 8, 15, 8, 3, 986, DateTimeKind.Utc).AddTicks(7737) });
        }
    }
}
