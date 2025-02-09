using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementServer.Migrations
{
    public partial class SeedAdminUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 8, 15, 5, 59, 213, DateTimeKind.Utc).AddTicks(7815), new DateTime(2025, 2, 8, 15, 5, 59, 213, DateTimeKind.Utc).AddTicks(8173) });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "StatusID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 2, 8, 15, 5, 59, 215, DateTimeKind.Utc).AddTicks(3616), new DateTime(2025, 2, 8, 15, 5, 59, 215, DateTimeKind.Utc).AddTicks(3990) });
        }
    }
}
