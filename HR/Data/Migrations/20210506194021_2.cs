using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HR.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SocialStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocialStatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "GenderId", "SecurityStamp", "SocialStatusId" },
                values: new object[] { new DateTime(2021, 5, 6, 22, 40, 20, 649, DateTimeKind.Local).AddTicks(6341), "2e107317-5cd1-4720-857e-c729561b0dac", 1, "93b454f6-f843-4805-ab49-5b68e2417e2e", 1 });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 6, 22, 40, 20, 652, DateTimeKind.Local).AddTicks(502));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SocialStatusId",
                table: "AspNetUsers",
                column: "SocialStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SocialStatus_SocialStatusId",
                table: "AspNetUsers",
                column: "SocialStatusId",
                principalTable: "SocialStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SocialStatus_SocialStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SocialStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SocialStatusId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0",
                columns: new[] { "Bday", "ConcurrencyStamp", "Gender", "SecurityStamp", "SocialStatus" },
                values: new object[] { new DateTime(2021, 5, 6, 21, 10, 34, 282, DateTimeKind.Local).AddTicks(6760), "05aaeb8c-7972-4898-98d2-63bc24e3a984", "M", "612d918a-a1dd-45cb-8fbb-eb404fca2227", "Married" });

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "MngStartDate",
                value: new DateTime(2021, 5, 6, 21, 10, 34, 285, DateTimeKind.Local).AddTicks(1634));
        }
    }
}
