using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _134 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 17, 18, 8, 12, 285, DateTimeKind.Local).AddTicks(8765), "aa66a601-7c56-4b5e-9c16-01fa8f08c90d", "72bea10b-158e-444a-be79-97405ba731f1" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 17, 18, 8, 12, 288, DateTimeKind.Local).AddTicks(4271));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 16, 19, 13, 38, 529, DateTimeKind.Local).AddTicks(6041), "022d97ef-629b-4fcf-a1ac-7076fc8e0456", "5a06c467-2c70-4004-9af0-0a2a0e567dda" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 16, 19, 13, 38, 532, DateTimeKind.Local).AddTicks(1262));
        }
    }
}
