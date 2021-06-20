using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
