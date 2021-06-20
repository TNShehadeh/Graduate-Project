using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class BankId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BankInfo",
                table: "BankInfo");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BankInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BankInfo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankInfo",
                table: "BankInfo",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 8, 2, 27, 54, 227, DateTimeKind.Local).AddTicks(400), "bf8a7bd2-77f4-4db1-bf3a-57a855dcf19d", "45652c4a-5ca2-4abc-a8f3-ceb516e2b1af" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 8, 2, 27, 54, 229, DateTimeKind.Local).AddTicks(5097));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BankInfo",
                table: "BankInfo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BankInfo");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BankInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankInfo",
                table: "BankInfo",
                column: "AccountNumber");

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
    }
}
