using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class end : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveDocumentPath",
                table: "LeaveApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 6, 4, 16, 52, 18, 625, DateTimeKind.Local).AddTicks(8781), "761f133b-dcad-4bb1-9a3e-280d8b19be56", "dbc608d0-e2df-4127-b314-b169a67a66a5" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 6, 4, 16, 52, 18, 628, DateTimeKind.Local).AddTicks(4583));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveDocumentPath",
                table: "LeaveApplication");

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
    }
}
