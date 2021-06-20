using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class woStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplication_Status_StatusId",
                table: "LeaveApplication");

            migrationBuilder.DropIndex(
                name: "IX_LeaveApplication_StatusId",
                table: "LeaveApplication");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "LeaveApplication");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LeaveApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 12, 3, 2, 18, 842, DateTimeKind.Local).AddTicks(6172), "1bad105a-59ce-4934-b9b6-237490c8cc6a", "8e314f12-223c-43dc-aedc-a6659c0458a3" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 12, 3, 2, 18, 845, DateTimeKind.Local).AddTicks(707));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeaveApplication");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "LeaveApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 12, 2, 38, 49, 494, DateTimeKind.Local).AddTicks(192), "eaf13de8-78b8-41d0-a46e-e340fbe715f7", "b7fdcb27-6b10-445f-93b7-e6f51f548dde" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 12, 2, 38, 49, 496, DateTimeKind.Local).AddTicks(6473));

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplication_StatusId",
                table: "LeaveApplication",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplication_Status_StatusId",
                table: "LeaveApplication",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
