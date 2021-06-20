using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class date1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 13, 6, 40, 4, 653, DateTimeKind.Local).AddTicks(4480), "37b8f640-ac7c-412d-8535-fff462858b90", "037fe236-01fb-4234-8efa-e9afb2a05bd4" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 13, 6, 40, 4, 656, DateTimeKind.Local).AddTicks(315));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 13, 6, 0, 37, 360, DateTimeKind.Local).AddTicks(5656), "f08ea351-e47f-4478-aeba-2a47b042bef7", "3aad2180-3d0e-45ba-8ee7-ba6bf99ef3d4" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 13, 6, 0, 37, 363, DateTimeKind.Local).AddTicks(66));
        }
    }
}
