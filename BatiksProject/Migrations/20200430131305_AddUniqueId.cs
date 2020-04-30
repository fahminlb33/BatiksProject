using Microsoft.EntityFrameworkCore.Migrations;

namespace BatiksProject.Migrations
{
    public partial class AddUniqueId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batiks_Localities_LocalityName",
                table: "Batiks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localities",
                table: "Localities");

            migrationBuilder.DropIndex(
                name: "IX_Batiks_LocalityName",
                table: "Batiks");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin");

            migrationBuilder.DropColumn(
                name: "LocalityName",
                table: "Batiks");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Localities",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Localities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Batiks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localities",
                table: "Localities",
                column: "LocalityId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "Username" },
                values: new object[] { 1, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Batiks_LocalityId",
                table: "Batiks",
                column: "LocalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batiks_Localities_LocalityId",
                table: "Batiks",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "LocalityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batiks_Localities_LocalityId",
                table: "Batiks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localities",
                table: "Localities");

            migrationBuilder.DropIndex(
                name: "IX_Batiks_LocalityId",
                table: "Batiks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Localities");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Batiks");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Localities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "LocalityName",
                table: "Batiks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localities",
                table: "Localities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Batiks_LocalityName",
                table: "Batiks",
                column: "LocalityName");

            migrationBuilder.AddForeignKey(
                name: "FK_Batiks_Localities_LocalityName",
                table: "Batiks",
                column: "LocalityName",
                principalTable: "Localities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
