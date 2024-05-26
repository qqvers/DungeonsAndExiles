using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    Defence = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Defence = table.Column<int>(type: "integer", nullable: false),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    Defence = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BackpackId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Backpacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backpacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Backpacks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BackpackItem",
                columns: table => new
                {
                    BackpackId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackpackItem", x => new { x.BackpackId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_BackpackItem_Backpacks_BackpackId",
                        column: x => x.BackpackId,
                        principalTable: "Backpacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackpackItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentItem",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentItem", x => new { x.EquipmentId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_EquipmentItem_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Damage", "Defence", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("16dcd5a3-af03-4235-a892-a306a9f00e40"), 0, 50, "Titanium Armor", "Body Armor" },
                    { new Guid("26e626de-d534-460b-b340-c27786f77cad"), 0, 25, "Titanium Helmet", "Helmet" },
                    { new Guid("2cefec1d-85de-4772-8d0b-4d721cf1f4d2"), 0, 20, "Steel Gloves", "Gloves" },
                    { new Guid("31e7b831-d897-43bf-a90a-ae1dbd434630"), 0, 15, "Iron Gloves", "Gloves" },
                    { new Guid("4cc104c7-25ea-4e29-8ca4-b061c1c62880"), 30, 0, "Diamond Sword", "Weapon" },
                    { new Guid("5c919272-ba64-4bdc-9dbd-a20c08b5d190"), 0, 40, "Steel Armor", "Body Armor" },
                    { new Guid("6067f838-d4fd-4578-80b2-9c0323916e77"), 10, 0, "Basic Sword", "Weapon" },
                    { new Guid("79f1f26f-abb3-47e1-bf4c-398f9247bb3a"), 0, 20, "Steel Helmet", "Helmet" },
                    { new Guid("7cda34ba-370b-4cf2-8a42-230199636a04"), 15, 0, "Iron Sword", "Weapon" },
                    { new Guid("8215e5f4-5f9f-49fc-aa30-3827ab6f8e77"), 0, 15, "Iron Boots", "Boots" },
                    { new Guid("84405d79-bcfc-4dd6-ae47-de1aee663791"), 0, 5, "Basic Helmet", "Helmet" },
                    { new Guid("8dfe2d2e-c7ff-4cbd-8340-77a0bbf68fad"), 0, 10, "Leather Helmet", "Helmet" },
                    { new Guid("94b2338c-4964-479f-bed4-2ccf4c24e878"), 0, 5, "Basic Gloves", "Gloves" },
                    { new Guid("a52ee975-52b6-46be-8b27-093a4be5ba7a"), 0, 20, "Leather Armor", "Body Armor" },
                    { new Guid("a6043ad9-7e4a-4548-9932-8d6068c75ef8"), 0, 30, "Iron Armor", "Body Armor" },
                    { new Guid("a6c507dd-62e9-432e-884f-602838a93a15"), 20, 0, "Steel Sword", "Weapon" },
                    { new Guid("c8afbadd-720a-4109-9a4f-b6732f588427"), 0, 5, "Basic Boots", "Boots" },
                    { new Guid("d062538c-9940-4f00-bea5-d1fb06e9ddc2"), 0, 25, "Titanium Gloves", "Gloves" },
                    { new Guid("d80284ce-4dda-4432-b189-a576a1975c01"), 0, 10, "Leather Boots", "Boots" },
                    { new Guid("d974bd95-2e28-4fe6-a1e0-cc34a1a788e0"), 0, 10, "Leather Gloves", "Gloves" },
                    { new Guid("dda70cb8-3eea-4f78-80ac-196f6c23a546"), 0, 25, "Titanium Boots", "Boots" },
                    { new Guid("e0205c66-f774-46dc-a096-080f8bd8cd5e"), 0, 15, "Iron Helmet", "Helmet" },
                    { new Guid("f3d5727f-45c5-469b-ade2-ed60b47c0e42"), 0, 20, "Steel Boots", "Boots" },
                    { new Guid("f432e2fb-25f5-44fd-b9cd-c8de68ef46e2"), 25, 0, "Titanium Sword", "Weapon" },
                    { new Guid("f4d991ed-cf24-41cd-8d7a-6561797976ee"), 0, 10, "Basic Armor", "Body Armor" }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Damage", "Defence", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("00b39ffb-4694-420a-bd2e-7b9c932d3d4d"), 10, 5, 50, 5, "Deadeye" },
                    { new Guid("2c0f2fc1-c93c-4da1-b7b5-687b823be618"), 20, 10, 70, 10, "Assassin" },
                    { new Guid("323ba06b-2ec1-4609-9f23-9d40626d4424"), 40, 30, 150, 20, "Elementalist" },
                    { new Guid("54daf5fb-2c39-4931-a8a6-f136ac2c645b"), 30, 15, 75, 15, "Occultist" },
                    { new Guid("c6b08855-2d05-47a1-b369-54e045822f65"), 100, 50, 300, 25, "Juggernaut" },
                    { new Guid("ed660b52-a502-43ce-9549-c6e5d6ed8072"), 5, 1, 30, 1, "Witch" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4"), "User" },
                    { new Guid("dd65bd59-a483-485f-adde-4eb624ddffc4"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("211a97a6-f3c1-4544-9fc1-fc7c07ff3af3"), "chris@wilson.com", "Chris", "$2a$10$sTiKenzm52qe9n18HZg23e/WCfA6HAYSR5eeWJKMkDHIswsdL9VUm", new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4") },
                    { new Guid("2d172af7-5f88-4edc-9242-155ec2fc3d75"), "john@doe.com", "John", "$2a$10$W9uxDkyq2GrVG3EcKJfqw.zDW2Ouip2qGGWIEZ4acMcXGWF/9g2LO", new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4") },
                    { new Guid("9855d8e0-1756-4ae4-a6aa-8822c9821d5f"), "admin@admin.com", "Admin", "$2a$10$bpdj5yy6B7q.FkwWkTL9QO.mA/o/xUEJw5xPOqcwZr6rcwybuxHqe", new Guid("dd65bd59-a483-485f-adde-4eb624ddffc4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackpackItem_ItemId",
                table: "BackpackItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Backpacks_PlayerId",
                table: "Backpacks",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItem_ItemId",
                table: "EquipmentItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_PlayerId",
                table: "Equipments",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackpackItem");

            migrationBuilder.DropTable(
                name: "EquipmentItem");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Backpacks");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
