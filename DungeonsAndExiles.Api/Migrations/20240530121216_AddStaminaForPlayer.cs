using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    public partial class AddStaminaForPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Damage", "Defence", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("0ad01736-adcc-4636-ae49-9edf92e6d3c7"), 0, 30, "Iron Armor", "Body Armor" },
                    { new Guid("0d6ebed0-22a8-4c9c-84d2-3a554a78b830"), 15, 0, "Iron Sword", "Weapon" },
                    { new Guid("288639ab-2fb9-42aa-8813-848410413986"), 0, 10, "Leather Helmet", "Helmet" },
                    { new Guid("2b23b799-6d83-4d46-bbc2-6d864dcd2df6"), 0, 5, "Basic Boots", "Boots" },
                    { new Guid("2cda8a7f-0640-4a5c-8cb5-3a3d60164d99"), 20, 0, "Steel Sword", "Weapon" },
                    { new Guid("3b00c739-5af0-44a3-b490-430347ddf37a"), 0, 50, "Titanium Armor", "Body Armor" },
                    { new Guid("42c9c7e0-796c-4ee7-ba3f-4c0258f128b0"), 0, 10, "Leather Boots", "Boots" },
                    { new Guid("448c8cd0-040d-43a4-9ee0-cd916ccd6a77"), 10, 0, "Basic Sword", "Weapon" },
                    { new Guid("53a39154-acfc-4e73-b176-9339de936d6e"), 0, 25, "Titanium Helmet", "Helmet" },
                    { new Guid("572ec2e8-7917-48c5-8a90-57c704335f5a"), 0, 15, "Iron Boots", "Boots" },
                    { new Guid("582eb83f-90ec-4d71-8c2c-b695b88aee41"), 25, 0, "Titanium Sword", "Weapon" },
                    { new Guid("5ad29be6-96b8-49d9-b842-c13e1162a71f"), 0, 25, "Titanium Boots", "Boots" },
                    { new Guid("5bf861bf-345b-4d88-a532-ed4dd4b4dc51"), 0, 20, "Steel Boots", "Boots" },
                    { new Guid("60f64182-6af4-4eae-aff0-075694aba774"), 0, 5, "Basic Helmet", "Helmet" },
                    { new Guid("62be2166-ca43-4b07-a721-89e5ca46644f"), 0, 10, "Basic Armor", "Body Armor" },
                    { new Guid("689f511b-4f71-4158-8328-0393c8bab9af"), 30, 0, "Diamond Sword", "Weapon" },
                    { new Guid("6940a620-9a00-4e04-b786-026d9bff8542"), 0, 15, "Iron Helmet", "Helmet" },
                    { new Guid("8741c040-de50-4220-a312-f715c4b57027"), 0, 20, "Steel Gloves", "Gloves" },
                    { new Guid("9420a79e-6666-4028-95cf-1facbf19ab77"), 0, 15, "Iron Gloves", "Gloves" },
                    { new Guid("9ac64752-61d9-438e-ba1e-e2894b564ee9"), 0, 5, "Basic Gloves", "Gloves" },
                    { new Guid("a12f38ab-be4d-43a9-9e3f-71d2da615ae0"), 0, 25, "Titanium Gloves", "Gloves" },
                    { new Guid("af46502c-de0d-4a7b-ad6d-4fb2d6bc62cf"), 0, 10, "Leather Gloves", "Gloves" },
                    { new Guid("c89e3f38-9971-480a-9aaa-1368de12ef4d"), 0, 20, "Steel Helmet", "Helmet" },
                    { new Guid("ed3671d5-fa16-4e98-be57-f86216748768"), 0, 20, "Leather Armor", "Body Armor" },
                    { new Guid("f35dd188-a76c-46da-9578-ce4948d3785e"), 0, 40, "Steel Armor", "Body Armor" }
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Damage", "Defence", "Health", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("18b87d72-d7fa-461a-a0a0-7458de86ca74"), 30, 15, 75, 15, "Occultist" },
                    { new Guid("3261b5dc-615f-4e5f-a7d9-a758a5cb08bb"), 100, 50, 300, 25, "Juggernaut" },
                    { new Guid("532b1c5e-6d99-474f-82e3-253d49e8ad91"), 40, 30, 150, 20, "Elementalist" },
                    { new Guid("7933e43c-dafa-44c8-8e39-ffcd93c2afee"), 10, 5, 50, 5, "Deadeye" },
                    { new Guid("8122d8d9-7f3d-486b-85bb-4d4405f29fbe"), 5, 1, 30, 1, "Witch" },
                    { new Guid("e8c5f7f4-bf55-47ff-927b-8862f23bfbc9"), 20, 10, 70, 10, "Assassin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f33e895-d123-482a-aaab-57317e7f4a26"), "User" },
                    { new Guid("ca640947-7f3c-42f1-9c58-f9910dd1548a"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RefreshToken", "RefreshTokenExpiry", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0d505a84-bdf2-4aea-97a1-5a1caf2fe18b"), "admin@admin.com", "Admin", "$2a$10$MCqolAF0BeDsVXsCdTnsJu2jU/UYjDdzpAP10nEOl/VniPL.pb1DS", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ca640947-7f3c-42f1-9c58-f9910dd1548a") },
                    { new Guid("55117ef5-2cef-439b-9f4e-2ba8bdb0ac6f"), "john@doe.com", "John", "$2a$10$4gIj0xdc6E7V2jhmINOqsODUCYzxyuRbPI0ZCLnObzLEhk3A0kU1u", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f33e895-d123-482a-aaab-57317e7f4a26") },
                    { new Guid("d46b568d-f865-4f99-b6a4-06ba30f78d65"), "chris@wilson.com", "Chris", "$2a$10$ecIPRZch2DcswoRiL1hesu1dnPqtRHdGuXbiRd2H9XT.NtujUmtFW", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f33e895-d123-482a-aaab-57317e7f4a26") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0ad01736-adcc-4636-ae49-9edf92e6d3c7"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0d6ebed0-22a8-4c9c-84d2-3a554a78b830"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("288639ab-2fb9-42aa-8813-848410413986"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2b23b799-6d83-4d46-bbc2-6d864dcd2df6"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2cda8a7f-0640-4a5c-8cb5-3a3d60164d99"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("3b00c739-5af0-44a3-b490-430347ddf37a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("42c9c7e0-796c-4ee7-ba3f-4c0258f128b0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("448c8cd0-040d-43a4-9ee0-cd916ccd6a77"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("53a39154-acfc-4e73-b176-9339de936d6e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("572ec2e8-7917-48c5-8a90-57c704335f5a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("582eb83f-90ec-4d71-8c2c-b695b88aee41"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5ad29be6-96b8-49d9-b842-c13e1162a71f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5bf861bf-345b-4d88-a532-ed4dd4b4dc51"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("60f64182-6af4-4eae-aff0-075694aba774"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("62be2166-ca43-4b07-a721-89e5ca46644f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("689f511b-4f71-4158-8328-0393c8bab9af"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6940a620-9a00-4e04-b786-026d9bff8542"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8741c040-de50-4220-a312-f715c4b57027"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9420a79e-6666-4028-95cf-1facbf19ab77"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9ac64752-61d9-438e-ba1e-e2894b564ee9"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a12f38ab-be4d-43a9-9e3f-71d2da615ae0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("af46502c-de0d-4a7b-ad6d-4fb2d6bc62cf"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("c89e3f38-9971-480a-9aaa-1368de12ef4d"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("ed3671d5-fa16-4e98-be57-f86216748768"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f35dd188-a76c-46da-9578-ce4948d3785e"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("18b87d72-d7fa-461a-a0a0-7458de86ca74"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("3261b5dc-615f-4e5f-a7d9-a758a5cb08bb"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("532b1c5e-6d99-474f-82e3-253d49e8ad91"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("7933e43c-dafa-44c8-8e39-ffcd93c2afee"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("8122d8d9-7f3d-486b-85bb-4d4405f29fbe"));

            migrationBuilder.DeleteData(
                table: "Monsters",
                keyColumn: "Id",
                keyValue: new Guid("e8c5f7f4-bf55-47ff-927b-8862f23bfbc9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0d505a84-bdf2-4aea-97a1-5a1caf2fe18b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55117ef5-2cef-439b-9f4e-2ba8bdb0ac6f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d46b568d-f865-4f99-b6a4-06ba30f78d65"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0f33e895-d123-482a-aaab-57317e7f4a26"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ca640947-7f3c-42f1-9c58-f9910dd1548a"));

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Players");

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
    }
}
