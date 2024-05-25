using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    public partial class Add_Monster_and_items_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Backpacks_BackpackId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Equipments_EquipmentId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Players_PlayerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_BackpackId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_EquipmentId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_PlayerId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("02d4a094-ba5a-407c-9910-0b51e0173c47"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0bb86019-052c-4566-8ddc-91e751280fa3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("17589fbe-168c-473f-a5de-c5fba8a442b2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("32872d90-3113-4575-ad73-c6625291197e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("4b84813c-7ba0-41e4-94a6-4c8e44609560"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("580a88fb-3e27-4df1-aa13-22b535629dbb"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5f984d9b-8624-4be9-8b17-4a0f55243128"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6e87c8be-14c5-470f-b367-bb952b57ec5f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("70359ae4-f9b3-45f6-97a4-99a80a0b1477"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("70ba45c4-25c9-47d7-a8f2-b12d0e9fcba5"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("795b1728-f1bc-484c-bbb2-29e6c3e65e40"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("7ceda71c-79b3-4de9-88d9-7bd3d5ecf304"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9d17a20e-a99f-4ffc-8a61-cedf6345d7a5"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a6181259-a4d2-47c3-a658-0042e0c92b21"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a6333169-730f-4b3e-a3eb-ddd07148099b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("aa203460-1324-4566-bd80-26a99bd7b51a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c9040b38-cf84-4a33-842f-089db199128b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("cb6e249f-be32-4afa-8881-132ad3c1fa0f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ce5c1e60-76ab-4a07-8dc9-26dba4359c81"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("cf29757e-83ac-4f6a-9bac-9bb8a15bf9e7"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f105205b-6a98-4c43-8a67-d59267d9eee7"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f41f0e89-d199-4a9b-bd19-dd8946526e83"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f4c5e971-2beb-4144-88a2-994297a63e4e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f7e35b7c-5dae-4ebb-94bc-f67de2279a2a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f9efd569-6faa-4958-bb27-deea0b21181d"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("220263dc-4a16-431e-a821-14f04fa5db5c"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("4b717bca-e76d-4400-bb8f-620584bfd830"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("50465e87-628b-4486-b7d7-2bd9f9649fac"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("5a7bb052-deb2-48c0-8ce1-e4cf6f4d38bf"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("7fafcf3b-256b-4136-957b-f4d19bd88bb3"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("a8e631cf-6b94-4f67-8a42-6317e554c43b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2368fa30-cf87-4d91-848a-b81c9f42e47b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9a5c9fe9-ead4-40a5-903c-e8774aa406d6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fad2c696-1dfb-40e8-9ac9-2986abb836f3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("847342c4-b807-460c-ad20-f4539669115b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ae2390a7-f320-4a62-a095-25eba1658b03"));

            migrationBuilder.DropColumn(
                name: "BackpackId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Items");

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
                    { new Guid("0f7fdf98-f115-46e0-a188-3bf93e13f4d5"), 0, 20, "Leather Armor", "Body Armor" },
                    { new Guid("107b0094-b5a2-4749-a2a1-9dc304ee9a40"), 0, 30, "Iron Armor", "Body Armor" },
                    { new Guid("14d02621-e7c6-4533-b071-42283e532607"), 0, 15, "Iron Gloves", "Gloves" },
                    { new Guid("1e8af1ca-e2c6-4476-bd53-40230e0d0688"), 10, 0, "Basic Sword", "Weapon" },
                    { new Guid("2f64247e-7355-4177-ad9c-7e2d209cf59e"), 0, 15, "Iron Helmet", "Helmet" },
                    { new Guid("2fccc62f-7c29-4fa1-ab67-8e60c110b966"), 0, 10, "Leather Boots", "Boots" },
                    { new Guid("3c9d0328-0b87-46e8-9dba-69d4094daf83"), 25, 0, "Titanium Sword", "Weapon" },
                    { new Guid("4099f361-3bd0-473e-b6e4-f2021d67b26c"), 0, 5, "Basic Boots", "Boots" },
                    { new Guid("48728490-cc31-44b4-8cc8-36bb3ad8cb68"), 0, 5, "Basic Helmet", "Helmet" },
                    { new Guid("572d4b5f-f0a5-4841-a2db-9b308b774332"), 0, 10, "Basic Armor", "Body Armor" },
                    { new Guid("5a2d11af-ff9b-4967-8210-5a44de70dfe8"), 0, 20, "Steel Helmet", "Helmet" },
                    { new Guid("623a92fd-2d05-487c-9d8a-ddbc89e053c4"), 0, 50, "Titanium Armor", "Body Armor" },
                    { new Guid("7f6c270c-a2d8-4d94-b66e-a1d10cf910f1"), 0, 10, "Leather Gloves", "Gloves" },
                    { new Guid("86f3ea29-6b49-4c7f-8fcc-4906b43b0d37"), 0, 40, "Steel Armor", "Body Armor" },
                    { new Guid("8e466a6f-110e-4973-8365-12833a838200"), 0, 25, "Titanium Boots", "Boots" },
                    { new Guid("9525f977-2877-4843-86d3-70422d00fcf0"), 30, 0, "Diamond Sword", "Weapon" },
                    { new Guid("a039b7f7-2fab-4368-9faf-51094430f514"), 20, 0, "Steel Sword", "Weapon" },
                    { new Guid("a8b7ae30-9169-41c1-9c80-73a691fcda58"), 0, 25, "Titanium Gloves", "Gloves" },
                    { new Guid("abd59e31-25e4-4f03-939e-e20367efd2ed"), 15, 0, "Iron Sword", "Weapon" },
                    { new Guid("b994235a-a9aa-4171-8921-a93a6d5ddd2f"), 0, 20, "Steel Gloves", "Gloves" },
                    { new Guid("bb70a4c9-489b-4940-967e-76ea3b8a9c2d"), 0, 15, "Iron Boots", "Boots" },
                    { new Guid("c59c3109-6dd2-450c-acfe-deb2d1e54686"), 0, 20, "Steel Boots", "Boots" },
                    { new Guid("dd44a08d-ee0d-4551-997c-9f68c9a20de4"), 0, 25, "Titanium Helmet", "Helmet" },
                    { new Guid("e1de89f7-ac55-45e7-bcf9-fe969f9a4ec3"), 0, 5, "Basic Gloves", "Gloves" },
                    { new Guid("f4909d80-f3a1-4d65-88ad-d5de952df265"), 0, 10, "Leather Helmet", "Helmet" }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Damage", "Defence", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("0f1ae3e4-bd52-4506-b907-6d25120742ef"), 100, 50, 300, 25, "Juggernaut" },
                    { new Guid("20e2d81b-69ed-460e-b7ef-59fb5f83c8e0"), 30, 15, 75, 15, "Occultist" },
                    { new Guid("27cf3148-f88a-4bb2-9eb8-a8619c0f605d"), 10, 5, 50, 5, "Deadeye" },
                    { new Guid("69ec19bc-9dea-47f1-b795-fc1eca9859f3"), 5, 1, 30, 1, "Witch" },
                    { new Guid("8a88dc07-c4d1-4961-823d-01be4c41ea1e"), 40, 30, 150, 20, "Elementalist" },
                    { new Guid("b7aa4f42-5d07-49a6-9765-f29c1731ff7c"), 20, 10, 70, 10, "Assassin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2d07e175-36bc-4c0f-af96-aae5e46053e9"), "User" },
                    { new Guid("b357bb36-eeb2-4744-b1a8-4d65faa728ee"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("07a6368c-57cb-4d02-b313-8839d58804ba"), "john@doe.com", "John", "$2a$10$XrOGCpg5LSOHgAmmzCYFwuBJwyY93.cgiHog9GijfQScdSpmGo7DO", new Guid("2d07e175-36bc-4c0f-af96-aae5e46053e9") },
                    { new Guid("649a6c45-8cd2-4442-b65d-80e72bb9c1a2"), "admin@admin.com", "Admin", "$2a$10$JGiFivsE2xIHTHkFECH3fux6ZRXikmepbUSr2.y6L8KDwoNVHyGDW", new Guid("b357bb36-eeb2-4744-b1a8-4d65faa728ee") },
                    { new Guid("f1b0f441-e05e-4096-88cb-aab441cb7030"), "chris@wilson.com", "Chris", "$2a$10$0cCd8qZP7VkLnPfNg9DG4uW4UvKVXMsdN7n4/BCkGgGojxjbOlcQa", new Guid("2d07e175-36bc-4c0f-af96-aae5e46053e9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackpackItem_ItemId",
                table: "BackpackItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItem_ItemId",
                table: "EquipmentItem",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackpackItem");

            migrationBuilder.DropTable(
                name: "EquipmentItem");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0f7fdf98-f115-46e0-a188-3bf93e13f4d5"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("107b0094-b5a2-4749-a2a1-9dc304ee9a40"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("14d02621-e7c6-4533-b071-42283e532607"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("1e8af1ca-e2c6-4476-bd53-40230e0d0688"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2f64247e-7355-4177-ad9c-7e2d209cf59e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2fccc62f-7c29-4fa1-ab67-8e60c110b966"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("3c9d0328-0b87-46e8-9dba-69d4094daf83"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("4099f361-3bd0-473e-b6e4-f2021d67b26c"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("48728490-cc31-44b4-8cc8-36bb3ad8cb68"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("572d4b5f-f0a5-4841-a2db-9b308b774332"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5a2d11af-ff9b-4967-8210-5a44de70dfe8"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("623a92fd-2d05-487c-9d8a-ddbc89e053c4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("7f6c270c-a2d8-4d94-b66e-a1d10cf910f1"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("86f3ea29-6b49-4c7f-8fcc-4906b43b0d37"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8e466a6f-110e-4973-8365-12833a838200"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9525f977-2877-4843-86d3-70422d00fcf0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a039b7f7-2fab-4368-9faf-51094430f514"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a8b7ae30-9169-41c1-9c80-73a691fcda58"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("abd59e31-25e4-4f03-939e-e20367efd2ed"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b994235a-a9aa-4171-8921-a93a6d5ddd2f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("bb70a4c9-489b-4940-967e-76ea3b8a9c2d"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c59c3109-6dd2-450c-acfe-deb2d1e54686"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("dd44a08d-ee0d-4551-997c-9f68c9a20de4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("e1de89f7-ac55-45e7-bcf9-fe969f9a4ec3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f4909d80-f3a1-4d65-88ad-d5de952df265"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("0f1ae3e4-bd52-4506-b907-6d25120742ef"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("20e2d81b-69ed-460e-b7ef-59fb5f83c8e0"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("27cf3148-f88a-4bb2-9eb8-a8619c0f605d"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("69ec19bc-9dea-47f1-b795-fc1eca9859f3"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("8a88dc07-c4d1-4961-823d-01be4c41ea1e"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("b7aa4f42-5d07-49a6-9765-f29c1731ff7c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("07a6368c-57cb-4d02-b313-8839d58804ba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("649a6c45-8cd2-4442-b65d-80e72bb9c1a2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1b0f441-e05e-4096-88cb-aab441cb7030"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2d07e175-36bc-4c0f-af96-aae5e46053e9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b357bb36-eeb2-4744-b1a8-4d65faa728ee"));

            migrationBuilder.AddColumn<Guid>(
                name: "BackpackId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "BackpackId", "Damage", "Defence", "EquipmentId", "Name", "PlayerId", "Type" },
                values: new object[,]
                {
                    { new Guid("02d4a094-ba5a-407c-9910-0b51e0173c47"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 15, new Guid("00000000-0000-0000-0000-000000000000"), "Iron Gloves", new Guid("00000000-0000-0000-0000-000000000000"), "Gloves" },
                    { new Guid("0bb86019-052c-4566-8ddc-91e751280fa3"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 5, new Guid("00000000-0000-0000-0000-000000000000"), "Basic Helmet", new Guid("00000000-0000-0000-0000-000000000000"), "Helmet" },
                    { new Guid("17589fbe-168c-473f-a5de-c5fba8a442b2"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 10, new Guid("00000000-0000-0000-0000-000000000000"), "Leather Boots", new Guid("00000000-0000-0000-0000-000000000000"), "Boots" },
                    { new Guid("32872d90-3113-4575-ad73-c6625291197e"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 5, new Guid("00000000-0000-0000-0000-000000000000"), "Basic Boots", new Guid("00000000-0000-0000-0000-000000000000"), "Boots" },
                    { new Guid("4b84813c-7ba0-41e4-94a6-4c8e44609560"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 10, new Guid("00000000-0000-0000-0000-000000000000"), "Leather Helmet", new Guid("00000000-0000-0000-0000-000000000000"), "Helmet" },
                    { new Guid("580a88fb-3e27-4df1-aa13-22b535629dbb"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 20, new Guid("00000000-0000-0000-0000-000000000000"), "Steel Gloves", new Guid("00000000-0000-0000-0000-000000000000"), "Gloves" },
                    { new Guid("5f984d9b-8624-4be9-8b17-4a0f55243128"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 30, new Guid("00000000-0000-0000-0000-000000000000"), "Iron Armor", new Guid("00000000-0000-0000-0000-000000000000"), "Body Armor" },
                    { new Guid("6e87c8be-14c5-470f-b367-bb952b57ec5f"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 25, new Guid("00000000-0000-0000-0000-000000000000"), "Titanium Helmet", new Guid("00000000-0000-0000-0000-000000000000"), "Helmet" },
                    { new Guid("70359ae4-f9b3-45f6-97a4-99a80a0b1477"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 50, new Guid("00000000-0000-0000-0000-000000000000"), "Titanium Armor", new Guid("00000000-0000-0000-0000-000000000000"), "Body Armor" },
                    { new Guid("70ba45c4-25c9-47d7-a8f2-b12d0e9fcba5"), new Guid("00000000-0000-0000-0000-000000000000"), 30, 0, new Guid("00000000-0000-0000-0000-000000000000"), "Diamond Sword", new Guid("00000000-0000-0000-0000-000000000000"), "Weapon" },
                    { new Guid("795b1728-f1bc-484c-bbb2-29e6c3e65e40"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 40, new Guid("00000000-0000-0000-0000-000000000000"), "Steel Armor", new Guid("00000000-0000-0000-0000-000000000000"), "Body Armor" },
                    { new Guid("7ceda71c-79b3-4de9-88d9-7bd3d5ecf304"), new Guid("00000000-0000-0000-0000-000000000000"), 10, 0, new Guid("00000000-0000-0000-0000-000000000000"), "Basic Sword", new Guid("00000000-0000-0000-0000-000000000000"), "Weapon" },
                    { new Guid("9d17a20e-a99f-4ffc-8a61-cedf6345d7a5"), new Guid("00000000-0000-0000-0000-000000000000"), 15, 0, new Guid("00000000-0000-0000-0000-000000000000"), "Iron Sword", new Guid("00000000-0000-0000-0000-000000000000"), "Weapon" },
                    { new Guid("a6181259-a4d2-47c3-a658-0042e0c92b21"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 20, new Guid("00000000-0000-0000-0000-000000000000"), "Steel Helmet", new Guid("00000000-0000-0000-0000-000000000000"), "Helmet" },
                    { new Guid("a6333169-730f-4b3e-a3eb-ddd07148099b"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 15, new Guid("00000000-0000-0000-0000-000000000000"), "Iron Boots", new Guid("00000000-0000-0000-0000-000000000000"), "Boots" },
                    { new Guid("aa203460-1324-4566-bd80-26a99bd7b51a"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 20, new Guid("00000000-0000-0000-0000-000000000000"), "Steel Boots", new Guid("00000000-0000-0000-0000-000000000000"), "Boots" },
                    { new Guid("c9040b38-cf84-4a33-842f-089db199128b"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 10, new Guid("00000000-0000-0000-0000-000000000000"), "Basic Armor", new Guid("00000000-0000-0000-0000-000000000000"), "Body Armor" },
                    { new Guid("cb6e249f-be32-4afa-8881-132ad3c1fa0f"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 20, new Guid("00000000-0000-0000-0000-000000000000"), "Leather Armor", new Guid("00000000-0000-0000-0000-000000000000"), "Body Armor" },
                    { new Guid("ce5c1e60-76ab-4a07-8dc9-26dba4359c81"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 15, new Guid("00000000-0000-0000-0000-000000000000"), "Iron Helmet", new Guid("00000000-0000-0000-0000-000000000000"), "Helmet" },
                    { new Guid("cf29757e-83ac-4f6a-9bac-9bb8a15bf9e7"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 10, new Guid("00000000-0000-0000-0000-000000000000"), "Leather Gloves", new Guid("00000000-0000-0000-0000-000000000000"), "Gloves" },
                    { new Guid("f105205b-6a98-4c43-8a67-d59267d9eee7"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 5, new Guid("00000000-0000-0000-0000-000000000000"), "Basic Gloves", new Guid("00000000-0000-0000-0000-000000000000"), "Gloves" },
                    { new Guid("f41f0e89-d199-4a9b-bd19-dd8946526e83"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 25, new Guid("00000000-0000-0000-0000-000000000000"), "Titanium Gloves", new Guid("00000000-0000-0000-0000-000000000000"), "Gloves" },
                    { new Guid("f4c5e971-2beb-4144-88a2-994297a63e4e"), new Guid("00000000-0000-0000-0000-000000000000"), 0, 25, new Guid("00000000-0000-0000-0000-000000000000"), "Titanium Boots", new Guid("00000000-0000-0000-0000-000000000000"), "Boots" },
                    { new Guid("f7e35b7c-5dae-4ebb-94bc-f67de2279a2a"), new Guid("00000000-0000-0000-0000-000000000000"), 25, 0, new Guid("00000000-0000-0000-0000-000000000000"), "Titanium Sword", new Guid("00000000-0000-0000-0000-000000000000"), "Weapon" },
                    { new Guid("f9efd569-6faa-4958-bb27-deea0b21181d"), new Guid("00000000-0000-0000-0000-000000000000"), 20, 0, new Guid("00000000-0000-0000-0000-000000000000"), "Steel Sword", new Guid("00000000-0000-0000-0000-000000000000"), "Weapon" }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Damage", "Defence", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("220263dc-4a16-431e-a821-14f04fa5db5c"), 10, 5, 50, 5, "Deadeye" },
                    { new Guid("4b717bca-e76d-4400-bb8f-620584bfd830"), 40, 30, 150, 20, "Elementalist" },
                    { new Guid("50465e87-628b-4486-b7d7-2bd9f9649fac"), 30, 15, 75, 15, "Occultist" },
                    { new Guid("5a7bb052-deb2-48c0-8ce1-e4cf6f4d38bf"), 100, 50, 300, 25, "Juggernaut" },
                    { new Guid("7fafcf3b-256b-4136-957b-f4d19bd88bb3"), 5, 1, 30, 1, "Witch" },
                    { new Guid("a8e631cf-6b94-4f67-8a42-6317e554c43b"), 20, 10, 70, 10, "Assassin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("847342c4-b807-460c-ad20-f4539669115b"), "User" },
                    { new Guid("ae2390a7-f320-4a62-a095-25eba1658b03"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("2368fa30-cf87-4d91-848a-b81c9f42e47b"), "chris@wilson.com", "Chris", "$2a$10$58wYwRBJs7G.LgZtgA/qR.Y0RCwKysX/twAAPk6tgVtSe3Yc7Ld56", new Guid("847342c4-b807-460c-ad20-f4539669115b") },
                    { new Guid("9a5c9fe9-ead4-40a5-903c-e8774aa406d6"), "john@doe.com", "John", "$2a$10$aJ1dkx4hXyzI1yo1W2TuoewEY.FAXqvmBkWXwYd0l5Iyngmj7QeNu", new Guid("847342c4-b807-460c-ad20-f4539669115b") },
                    { new Guid("fad2c696-1dfb-40e8-9ac9-2986abb836f3"), "admin@admin.com", "Admin", "$2a$10$lJJzjLX.bc24bVOivstHCOnQ4xa9ay35FjMwsZ1BL/L3Au1HWmE9S", new Guid("ae2390a7-f320-4a62-a095-25eba1658b03") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BackpackId",
                table: "Items",
                column: "BackpackId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EquipmentId",
                table: "Items",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PlayerId",
                table: "Items",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Backpacks_BackpackId",
                table: "Items",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Equipments_EquipmentId",
                table: "Items",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Players_PlayerId",
                table: "Items",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
