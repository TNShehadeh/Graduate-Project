using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LeaveApplication");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "LeaveApplication");

            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "LeaveApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

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
                name: "IX_LeaveApplication_LeaveTypeId",
                table: "LeaveApplication",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplication_StatusId",
                table: "LeaveApplication",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplication_LeaveType_LeaveTypeId",
                table: "LeaveApplication",
                column: "LeaveTypeId",
                principalTable: "LeaveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplication_Status_StatusId",
                table: "LeaveApplication",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplication_LeaveType_LeaveTypeId",
                table: "LeaveApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplication_Status_StatusId",
                table: "LeaveApplication");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_LeaveApplication_LeaveTypeId",
                table: "LeaveApplication");

            migrationBuilder.DropIndex(
                name: "IX_LeaveApplication_StatusId",
                table: "LeaveApplication");

            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "LeaveApplication");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "LeaveApplication");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LeaveApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "LeaveApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 12, 1, 39, 26, 424, DateTimeKind.Local).AddTicks(1217), "1e438759-59ca-4a85-95e4-34aac827faf5", "354f0982-3cb0-44c9-953d-569e0553fdc8" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 12, 1, 39, 26, 426, DateTimeKind.Local).AddTicks(5943));
        }
    }
}
