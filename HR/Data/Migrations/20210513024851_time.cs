using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalVacations",
                table: "OfficialDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "LeaveCount",
                table: "LeaveApplication",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 13, 5, 48, 50, 806, DateTimeKind.Local).AddTicks(2979), "983ba33f-88b5-49d9-9d6a-34594c2a2450", "9e717b0a-a5fb-418a-bc2f-efa437718ed7" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 13, 5, 48, 50, 808, DateTimeKind.Local).AddTicks(8007));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalVacations",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveCount",
                table: "LeaveApplication",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

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
    }
}
