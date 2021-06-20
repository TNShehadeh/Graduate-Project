using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class DepartmentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bday", "ConcurrencyStamp", "DepartmentName", "Email", "EmailConfirmed", "FatherName", "FirstName", "Gender", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "SocialStatus", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0", 0, new DateTime(2021, 5, 4, 4, 37, 59, 77, DateTimeKind.Local).AddTicks(8891), "c145d1e9-dffc-4667-a21a-c083fab4844a", 1, "Admin@admin.com", true, "Admin", "Admin", "M", false, null, "Admiin", null, null, null, null, true, null, "e400f9a4-7f15-4b5c-a9a7-91353f0dce22", "Married", false, "Admin@admin.com" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "EmployeeId", "MngStartDate", "Name" },
                values: new object[] { 1, "0", new DateTime(2021, 5, 4, 4, 37, 59, 80, DateTimeKind.Local).AddTicks(5038), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0");
        }
    }
}
