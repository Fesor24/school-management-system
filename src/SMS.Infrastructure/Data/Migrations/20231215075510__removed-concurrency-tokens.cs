using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _removedconcurrencytokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Department_DepartmentId",
                schema: "sms",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "sms",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "sms",
                table: "Course");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                schema: "sms",
                table: "Course",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Department_DepartmentId",
                schema: "sms",
                table: "Course",
                column: "DepartmentId",
                principalSchema: "sms",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Department_DepartmentId",
                schema: "sms",
                table: "Course");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                schema: "sms",
                table: "Department",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                schema: "sms",
                table: "Course",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                schema: "sms",
                table: "Course",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Department_DepartmentId",
                schema: "sms",
                table: "Course",
                column: "DepartmentId",
                principalSchema: "sms",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
