using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ODate",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ODate",
                table: "Notification");

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
    }
}
