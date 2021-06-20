using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class TotalVacations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVacations",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 7, 21, 30, 55, 762, DateTimeKind.Local).AddTicks(2904), "22e71e16-97c2-479a-b1c8-82228f8de373", "0f8c55f4-f9c3-4377-86c7-b5dd49db6509" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 7, 21, 30, 55, 764, DateTimeKind.Local).AddTicks(7622));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVacations",
                table: "OfficialDetails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 6, 22, 40, 20, 649, DateTimeKind.Local).AddTicks(6341), "2e107317-5cd1-4720-857e-c729561b0dac", "93b454f6-f843-4805-ab49-5b68e2417e2e" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 6, 22, 40, 20, 652, DateTimeKind.Local).AddTicks(502));
        }
    }
}
