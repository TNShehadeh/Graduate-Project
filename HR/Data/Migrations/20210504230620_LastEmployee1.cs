using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class LastEmployee1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shift",
                table: "OfficialDetails",
                newName: "ShiftName");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                table: "OfficialDetails",
                newName: "EmployeeTypeName");

            migrationBuilder.RenameColumn(
                name: "EmlpoyeeGrade",
                table: "OfficialDetails",
                newName: "EmlpoyeeGradeName");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "OfficialDetails",
                newName: "DesignationName");

            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "OfficialDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmlpoyeeGradeId",
                table: "OfficialDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "OfficialDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "OfficialDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 5, 2, 6, 19, 816, DateTimeKind.Local).AddTicks(4433), "455f8c7a-78d3-4a1d-8084-7d9e3c04fefd", "c7c0de10-4bb3-4c80-a0d3-13706ba27bdb" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 5, 2, 6, 19, 818, DateTimeKind.Local).AddTicks(9040));

            migrationBuilder.CreateIndex(
                name: "IX_OfficialDetails_DesignationId",
                table: "OfficialDetails",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialDetails_EmlpoyeeGradeId",
                table: "OfficialDetails",
                column: "EmlpoyeeGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialDetails_EmployeeTypeId",
                table: "OfficialDetails",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialDetails_ShiftId",
                table: "OfficialDetails",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_Designation_DesignationId",
                table: "OfficialDetails",
                column: "DesignationId",
                principalTable: "Designation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_EmlpoyeeGrade_EmlpoyeeGradeId",
                table: "OfficialDetails",
                column: "EmlpoyeeGradeId",
                principalTable: "EmlpoyeeGrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_EmployeeType_EmployeeTypeId",
                table: "OfficialDetails",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_Shift_ShiftId",
                table: "OfficialDetails",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDetails_Designation_DesignationId",
                table: "OfficialDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDetails_EmlpoyeeGrade_EmlpoyeeGradeId",
                table: "OfficialDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDetails_EmployeeType_EmployeeTypeId",
                table: "OfficialDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDetails_Shift_ShiftId",
                table: "OfficialDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfficialDetails_DesignationId",
                table: "OfficialDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfficialDetails_EmlpoyeeGradeId",
                table: "OfficialDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfficialDetails_EmployeeTypeId",
                table: "OfficialDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfficialDetails_ShiftId",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "EmlpoyeeGradeId",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "OfficialDetails");

            migrationBuilder.RenameColumn(
                name: "ShiftName",
                table: "OfficialDetails",
                newName: "Shift");

            migrationBuilder.RenameColumn(
                name: "EmployeeTypeName",
                table: "OfficialDetails",
                newName: "EmployeeType");

            migrationBuilder.RenameColumn(
                name: "EmlpoyeeGradeName",
                table: "OfficialDetails",
                newName: "EmlpoyeeGrade");

            migrationBuilder.RenameColumn(
                name: "DesignationName",
                table: "OfficialDetails",
                newName: "Designation");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 5, 1, 57, 37, 669, DateTimeKind.Local).AddTicks(6683), "60278a17-128c-40c7-84c8-37792ac5fe2d", "fd47a1d0-7616-41c3-af0c-7e46cdf455ff" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 5, 1, 57, 37, 672, DateTimeKind.Local).AddTicks(1526));
        }
    }
}
