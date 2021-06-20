using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class startEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Shift",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Shift",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 12, 0, 27, 21, 802, DateTimeKind.Local).AddTicks(1105), "787d9d09-856e-4db3-8112-affa23fb84ff", "2cd20803-fa8c-4347-8ae7-5bb4a434d15f" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 12, 0, 27, 21, 804, DateTimeKind.Local).AddTicks(4673));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Shift");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 8, 2, 27, 54, 227, DateTimeKind.Local).AddTicks(400), "bf8a7bd2-77f4-4db1-bf3a-57a855dcf19d", "45652c4a-5ca2-4abc-a8f3-ceb516e2b1af" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 8, 2, 27, 54, 229, DateTimeKind.Local).AddTicks(5097));
        }
    }
}
