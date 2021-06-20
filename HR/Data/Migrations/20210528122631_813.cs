using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _813 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sickleave",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 28, 15, 26, 30, 813, DateTimeKind.Local).AddTicks(2204), "1063e447-f0fa-46e5-aced-1f9a7cd40ce7", "9041004e-0386-4abf-91ec-d23364ca8902" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 28, 15, 26, 30, 815, DateTimeKind.Local).AddTicks(7990));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sickleave",
                table: "OfficialDetails");

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
    }
}
