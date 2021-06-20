using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class timr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 13, 5, 2, 31, 941, DateTimeKind.Local).AddTicks(1508), "570d5ae1-5287-4602-a3d0-237cda6558f7", "5b68b7e0-6ce0-480e-9c2e-01dff2b7f34e" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 13, 5, 2, 31, 943, DateTimeKind.Local).AddTicks(5610));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 12, 3, 2, 18, 842, DateTimeKind.Local).AddTicks(6172), "1bad105a-59ce-4934-b9b6-237490c8cc6a", "8e314f12-223c-43dc-aedc-a6659c0458a3" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 12, 3, 2, 18, 845, DateTimeKind.Local).AddTicks(707));
        }
    }
}
