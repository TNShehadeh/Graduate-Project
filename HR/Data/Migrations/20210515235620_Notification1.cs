using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class Notification1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToUser",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 16, 2, 56, 20, 181, DateTimeKind.Local).AddTicks(5403), "fd27f104-2152-4815-a50e-97a31f0f79e7", "9f86871a-eccb-4f26-928e-e12fa5f0e060" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 16, 2, 56, 20, 184, DateTimeKind.Local).AddTicks(1351));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToUser",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 16, 2, 50, 10, 848, DateTimeKind.Local).AddTicks(3625), "cafa77a9-0d04-4967-8652-c63ec149eff9", "9dc070d4-e51d-40ab-ba6f-57717886984a" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 16, 2, 50, 10, 851, DateTimeKind.Local).AddTicks(71));
        }
    }
}
