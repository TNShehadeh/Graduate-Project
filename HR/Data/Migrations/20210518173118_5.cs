using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SuperEmployeeInfo",
                table: "LeaveApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 18, 20, 31, 17, 983, DateTimeKind.Local).AddTicks(6527), "a2ee038a-245e-4b8c-ba7d-5b2b4c4ad975", "9ef81727-3431-4020-8268-aa3a935d3631" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 18, 20, 31, 17, 986, DateTimeKind.Local).AddTicks(2807));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuperEmployeeInfo",
                table: "LeaveApplication");

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
    }
}
