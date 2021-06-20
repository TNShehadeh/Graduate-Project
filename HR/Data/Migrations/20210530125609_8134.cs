using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _8134 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotoleHouresToleave",
                table: "OfficialDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HouresCount",
                table: "LeaveApplication",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 30, 15, 56, 8, 623, DateTimeKind.Local).AddTicks(4457), "6f32345b-d26b-4d58-8caf-7f16ca238252", "35b16695-05e9-4813-adbc-8a1c9bb949db" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 30, 15, 56, 8, 626, DateTimeKind.Local).AddTicks(106));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotoleHouresToleave",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "HouresCount",
                table: "LeaveApplication");

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
    }
}
