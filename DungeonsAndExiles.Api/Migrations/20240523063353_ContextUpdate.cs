using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    public partial class ContextUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cd0897c1-f361-4685-bd00-c3d56a0c8290"), "Admin" },
                    { new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("28649b79-c8ac-4b9f-909b-1f01cbd1174f"), "chris@wilson.com", "Chris", "$2a$10$O20fOAjEjgh3Fr.WEJXy5OR1dBUrtJCRRpLhzUMLby0UdaU.27EgO", new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0") },
                    { new Guid("5556db48-1b6d-462a-93ab-fb7a12209fd3"), "john@doe.com", "John", "$2a$10$ZBTfzq4rzBIdZQW4Af4XPeNwHn6FxRQknLH5/mP3s6v9.w7O2ykvW", new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0") },
                    { new Guid("ec997402-6b71-4112-a8b4-a3194ebabd0d"), "admin@admin.com", "Admin", "$2a$10$T/EDg0HHUC3ZZxb0WpDYjuSvdtOkzJYqxVJ4oAebI1US7PWmGS0V.", new Guid("cd0897c1-f361-4685-bd00-c3d56a0c8290") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28649b79-c8ac-4b9f-909b-1f01cbd1174f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5556db48-1b6d-462a-93ab-fb7a12209fd3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ec997402-6b71-4112-a8b4-a3194ebabd0d"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
