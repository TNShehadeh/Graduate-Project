using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class LastEmployee12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DesignationName",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "EmlpoyeeGradeName",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeName",
                table: "OfficialDetails");

            migrationBuilder.DropColumn(
                name: "ShiftName",
                table: "OfficialDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTypeId",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmlpoyeeGradeId",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignationId",
                table: "OfficialDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { new DateTime(2021, 5, 5, 2, 16, 2, 931, DateTimeKind.Local).AddTicks(2064), "747264c4-422d-48ee-a1bf-3f8e955e0874", "16dc00dd-05e7-43ca-a071-14aac14d4a2c" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 5, 2, 16, 2, 933, DateTimeKind.Local).AddTicks(6668));

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_Designation_DesignationId",
                table: "OfficialDetails",
                column: "DesignationId",
                principalTable: "Designation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_EmlpoyeeGrade_EmlpoyeeGradeId",
                table: "OfficialDetails",
                column: "EmlpoyeeGradeId",
                principalTable: "EmlpoyeeGrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_EmployeeType_EmployeeTypeId",
                table: "OfficialDetails",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDetails_Shift_ShiftId",
                table: "OfficialDetails",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "OfficialDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTypeId",
                table: "OfficialDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmlpoyeeGradeId",
                table: "OfficialDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DesignationId",
                table: "OfficialDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DesignationName",
                table: "OfficialDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmlpoyeeGradeName",
                table: "OfficialDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeTypeName",
                table: "OfficialDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShiftName",
                table: "OfficialDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
