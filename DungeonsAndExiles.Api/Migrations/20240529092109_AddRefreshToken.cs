using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("16dcd5a3-af03-4235-a892-a306a9f00e40"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("26e626de-d534-460b-b340-c27786f77cad"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2cefec1d-85de-4772-8d0b-4d721cf1f4d2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("31e7b831-d897-43bf-a90a-ae1dbd434630"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("4cc104c7-25ea-4e29-8ca4-b061c1c62880"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5c919272-ba64-4bdc-9dbd-a20c08b5d190"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6067f838-d4fd-4578-80b2-9c0323916e77"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("79f1f26f-abb3-47e1-bf4c-398f9247bb3a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("7cda34ba-370b-4cf2-8a42-230199636a04"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8215e5f4-5f9f-49fc-aa30-3827ab6f8e77"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("84405d79-bcfc-4dd6-ae47-de1aee663791"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8dfe2d2e-c7ff-4cbd-8340-77a0bbf68fad"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("94b2338c-4964-479f-bed4-2ccf4c24e878"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a52ee975-52b6-46be-8b27-093a4be5ba7a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a6043ad9-7e4a-4548-9932-8d6068c75ef8"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a6c507dd-62e9-432e-884f-602838a93a15"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c8afbadd-720a-4109-9a4f-b6732f588427"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d062538c-9940-4f00-bea5-d1fb06e9ddc2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d80284ce-4dda-4432-b189-a576a1975c01"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d974bd95-2e28-4fe6-a1e0-cc34a1a788e0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("dda70cb8-3eea-4f78-80ac-196f6c23a546"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("e0205c66-f774-46dc-a096-080f8bd8cd5e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f3d5727f-45c5-469b-ade2-ed60b47c0e42"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f432e2fb-25f5-44fd-b9cd-c8de68ef46e2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f4d991ed-cf24-41cd-8d7a-6561797976ee"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("00b39ffb-4694-420a-bd2e-7b9c932d3d4d"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("2c0f2fc1-c93c-4da1-b7b5-687b823be618"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("323ba06b-2ec1-4609-9f23-9d40626d4424"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("54daf5fb-2c39-4931-a8a6-f136ac2c645b"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("c6b08855-2d05-47a1-b369-54e045822f65"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("ed660b52-a502-43ce-9549-c6e5d6ed8072"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("211a97a6-f3c1-4544-9fc1-fc7c07ff3af3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2d172af7-5f88-4edc-9242-155ec2fc3d75"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9855d8e0-1756-4ae4-a6aa-8822c9821d5f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dd65bd59-a483-485f-adde-4eb624ddffc4"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Damage", "Defence", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("01bfec6b-3690-4910-9ce9-fa8abd58017b"), 0, 10, "Leather Gloves", "Gloves" },
                    { new Guid("147c03ee-f078-4b90-8523-13bea1cb2aca"), 10, 0, "Basic Sword", "Weapon" },
                    { new Guid("2225a7ab-b612-45c8-a412-f72a62985921"), 0, 10, "Basic Armor", "Body Armor" },
                    { new Guid("2f6d74a2-a309-4189-a838-180ff9c7ed7a"), 0, 25, "Titanium Boots", "Boots" },
                    { new Guid("3578569a-4f83-4e84-8310-7b0209bc9ab4"), 0, 20, "Steel Boots", "Boots" },
                    { new Guid("389b91c6-2455-4ba5-811f-6d7baf3a1dde"), 0, 25, "Titanium Helmet", "Helmet" },
                    { new Guid("44a1f559-1141-4d83-b0cc-a75dc08129d8"), 0, 10, "Leather Helmet", "Helmet" },
                    { new Guid("47f96b23-df9c-468f-93bb-c2c09c6d8771"), 25, 0, "Titanium Sword", "Weapon" },
                    { new Guid("4cce7c47-2453-41c9-a5ae-44acefa16348"), 0, 25, "Titanium Gloves", "Gloves" },
                    { new Guid("83a4b006-4f70-45ec-8747-fd6f23ebb905"), 0, 20, "Steel Gloves", "Gloves" },
                    { new Guid("84ec96f7-c52f-4d8c-9d23-3b11f3f0f455"), 0, 40, "Steel Armor", "Body Armor" },
                    { new Guid("88657229-5149-4a69-922d-0120ae485443"), 0, 20, "Leather Armor", "Body Armor" },
                    { new Guid("8b5b625b-1fa4-4bf0-9ef5-98b292bf9339"), 0, 5, "Basic Boots", "Boots" },
                    { new Guid("96dca947-5a8f-4a1e-9771-e76e3d692dc4"), 20, 0, "Steel Sword", "Weapon" },
                    { new Guid("a73a3735-303c-4bef-835e-dc94abe6d921"), 0, 10, "Leather Boots", "Boots" },
                    { new Guid("b3613013-6b20-48e5-b860-8baead982749"), 0, 15, "Iron Gloves", "Gloves" },
                    { new Guid("b6453488-a6d6-4023-9214-a535de404462"), 0, 30, "Iron Armor", "Body Armor" },
                    { new Guid("c8325ef8-9eef-4cce-915d-fea5b3d75bce"), 0, 20, "Steel Helmet", "Helmet" },
                    { new Guid("cab56cfd-0d16-4117-a5b8-b76bb81fa45e"), 30, 0, "Diamond Sword", "Weapon" },
                    { new Guid("e7cf9a88-8558-45b0-a4af-5eb997fad69e"), 0, 50, "Titanium Armor", "Body Armor" },
                    { new Guid("f09a7a96-44a4-47c6-8b02-35bbc29fc1ad"), 0, 5, "Basic Gloves", "Gloves" },
                    { new Guid("f91eb875-281f-46fe-8610-4bd90a113c0d"), 0, 15, "Iron Helmet", "Helmet" },
                    { new Guid("fa2595d5-818e-4162-88db-38785a35d767"), 0, 15, "Iron Boots", "Boots" },
                    { new Guid("fe4b4f27-b124-4c2d-9699-cbeee458902b"), 0, 5, "Basic Helmet", "Helmet" },
                    { new Guid("ff518512-7619-431c-8ad7-196bebbc93a2"), 15, 0, "Iron Sword", "Weapon" }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Damage", "Defence", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("2950121c-4c23-444e-8d50-3a27604ec120"), 20, 10, 70, 10, "Assassin" },
                    { new Guid("2df9dd43-0597-49f7-a5d8-5a0625dbdc0f"), 10, 5, 50, 5, "Deadeye" },
                    { new Guid("34399507-0167-4988-b9c1-a0f3c1bbb262"), 5, 1, 30, 1, "Witch" },
                    { new Guid("e808e923-75d7-4513-8349-4d0643ed1fb1"), 40, 30, 150, 20, "Elementalist" },
                    { new Guid("ee39bda6-9056-4151-865f-5eeccfaf34ec"), 30, 15, 75, 15, "Occultist" },
                    { new Guid("f4f80fb0-ac71-4e8e-974f-da6562d31491"), 100, 50, 300, 25, "Juggernaut" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ca494e53-13d7-4eb0-a3e6-08c341ee9a59"), "User" },
                    { new Guid("d86976c1-c91e-454a-95a0-f28c6e9ce50a"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RefreshToken", "RefreshTokenExpiry", "RoleId" },
                values: new object[,]
                {
                    { new Guid("08e98811-4841-4877-923e-d210defb473b"), "chris@wilson.com", "Chris", "$2a$10$N0SQfcoztF24QMDMjL/jbux.5z20qHtr2clwDPKx4GzrK9vFyBLKu", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ca494e53-13d7-4eb0-a3e6-08c341ee9a59") },
                    { new Guid("491efe03-17a4-40b4-bb99-21c879d7b5e4"), "john@doe.com", "John", "$2a$10$05y.Cf9DQ9H2sXhew1q72..3r.rkzJZEhhw8h1Ho/P/vVjoqqNCI6", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ca494e53-13d7-4eb0-a3e6-08c341ee9a59") },
                    { new Guid("68db6b58-f11c-4412-ab08-dfb11e822900"), "admin@admin.com", "Admin", "$2a$10$UFZ74rM4Tw4Pn5nXj7QaHOTEhfMWrmckFTjoKa775E4LpKEuMCBB6", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d86976c1-c91e-454a-95a0-f28c6e9ce50a") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("01bfec6b-3690-4910-9ce9-fa8abd58017b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("147c03ee-f078-4b90-8523-13bea1cb2aca"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2225a7ab-b612-45c8-a412-f72a62985921"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2f6d74a2-a309-4189-a838-180ff9c7ed7a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("3578569a-4f83-4e84-8310-7b0209bc9ab4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("389b91c6-2455-4ba5-811f-6d7baf3a1dde"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("44a1f559-1141-4d83-b0cc-a75dc08129d8"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("47f96b23-df9c-468f-93bb-c2c09c6d8771"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("4cce7c47-2453-41c9-a5ae-44acefa16348"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("83a4b006-4f70-45ec-8747-fd6f23ebb905"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("84ec96f7-c52f-4d8c-9d23-3b11f3f0f455"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("88657229-5149-4a69-922d-0120ae485443"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8b5b625b-1fa4-4bf0-9ef5-98b292bf9339"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("96dca947-5a8f-4a1e-9771-e76e3d692dc4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a73a3735-303c-4bef-835e-dc94abe6d921"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b3613013-6b20-48e5-b860-8baead982749"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b6453488-a6d6-4023-9214-a535de404462"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c8325ef8-9eef-4cce-915d-fea5b3d75bce"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("cab56cfd-0d16-4117-a5b8-b76bb81fa45e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("e7cf9a88-8558-45b0-a4af-5eb997fad69e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f09a7a96-44a4-47c6-8b02-35bbc29fc1ad"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f91eb875-281f-46fe-8610-4bd90a113c0d"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("fa2595d5-818e-4162-88db-38785a35d767"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("fe4b4f27-b124-4c2d-9699-cbeee458902b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ff518512-7619-431c-8ad7-196bebbc93a2"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("2950121c-4c23-444e-8d50-3a27604ec120"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("2df9dd43-0597-49f7-a5d8-5a0625dbdc0f"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("34399507-0167-4988-b9c1-a0f3c1bbb262"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("e808e923-75d7-4513-8349-4d0643ed1fb1"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("ee39bda6-9056-4151-865f-5eeccfaf34ec"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("f4f80fb0-ac71-4e8e-974f-da6562d31491"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08e98811-4841-4877-923e-d210defb473b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("491efe03-17a4-40b4-bb99-21c879d7b5e4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("68db6b58-f11c-4412-ab08-dfb11e822900"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ca494e53-13d7-4eb0-a3e6-08c341ee9a59"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d86976c1-c91e-454a-95a0-f28c6e9ce50a"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "Users");

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
        }
    }
}
