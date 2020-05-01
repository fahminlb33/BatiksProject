using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BatiksProject.Migrations
{
    public partial class UseFloatsFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinioObjectName",
                table: "Batiks");

            migrationBuilder.AlterColumn<string>(
                name: "Features",
                table: "Batiks",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "UploadName",
                table: "Batiks",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadName",
                table: "Batiks");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Features",
                table: "Batiks",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "MinioObjectName",
                table: "Batiks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
