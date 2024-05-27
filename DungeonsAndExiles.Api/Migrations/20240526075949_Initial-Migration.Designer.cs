﻿// <auto-generated />
using System;
using DungeonsAndExiles.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DungeonsAndExiles.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240526075949_Initial-Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackpackItem", b =>
                {
                    b.Property<Guid>("BackpackId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.HasKey("BackpackId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("BackpackItem");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Backpack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Backpacks");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("Defence")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = new Guid("94b2338c-4964-479f-bed4-2ccf4c24e878"),
                            Damage = 0,
                            Defence = 5,
                            Name = "Basic Gloves",
                            Type = "Gloves"
                        },
                        new
                        {
                            Id = new Guid("d974bd95-2e28-4fe6-a1e0-cc34a1a788e0"),
                            Damage = 0,
                            Defence = 10,
                            Name = "Leather Gloves",
                            Type = "Gloves"
                        },
                        new
                        {
                            Id = new Guid("31e7b831-d897-43bf-a90a-ae1dbd434630"),
                            Damage = 0,
                            Defence = 15,
                            Name = "Iron Gloves",
                            Type = "Gloves"
                        },
                        new
                        {
                            Id = new Guid("2cefec1d-85de-4772-8d0b-4d721cf1f4d2"),
                            Damage = 0,
                            Defence = 20,
                            Name = "Steel Gloves",
                            Type = "Gloves"
                        },
                        new
                        {
                            Id = new Guid("d062538c-9940-4f00-bea5-d1fb06e9ddc2"),
                            Damage = 0,
                            Defence = 25,
                            Name = "Titanium Gloves",
                            Type = "Gloves"
                        },
                        new
                        {
                            Id = new Guid("c8afbadd-720a-4109-9a4f-b6732f588427"),
                            Damage = 0,
                            Defence = 5,
                            Name = "Basic Boots",
                            Type = "Boots"
                        },
                        new
                        {
                            Id = new Guid("d80284ce-4dda-4432-b189-a576a1975c01"),
                            Damage = 0,
                            Defence = 10,
                            Name = "Leather Boots",
                            Type = "Boots"
                        },
                        new
                        {
                            Id = new Guid("8215e5f4-5f9f-49fc-aa30-3827ab6f8e77"),
                            Damage = 0,
                            Defence = 15,
                            Name = "Iron Boots",
                            Type = "Boots"
                        },
                        new
                        {
                            Id = new Guid("f3d5727f-45c5-469b-ade2-ed60b47c0e42"),
                            Damage = 0,
                            Defence = 20,
                            Name = "Steel Boots",
                            Type = "Boots"
                        },
                        new
                        {
                            Id = new Guid("dda70cb8-3eea-4f78-80ac-196f6c23a546"),
                            Damage = 0,
                            Defence = 25,
                            Name = "Titanium Boots",
                            Type = "Boots"
                        },
                        new
                        {
                            Id = new Guid("6067f838-d4fd-4578-80b2-9c0323916e77"),
                            Damage = 10,
                            Defence = 0,
                            Name = "Basic Sword",
                            Type = "Weapon"
                        },
                        new
                        {
                            Id = new Guid("7cda34ba-370b-4cf2-8a42-230199636a04"),
                            Damage = 15,
                            Defence = 0,
                            Name = "Iron Sword",
                            Type = "Weapon"
                        },
                        new
                        {
                            Id = new Guid("a6c507dd-62e9-432e-884f-602838a93a15"),
                            Damage = 20,
                            Defence = 0,
                            Name = "Steel Sword",
                            Type = "Weapon"
                        },
                        new
                        {
                            Id = new Guid("f432e2fb-25f5-44fd-b9cd-c8de68ef46e2"),
                            Damage = 25,
                            Defence = 0,
                            Name = "Titanium Sword",
                            Type = "Weapon"
                        },
                        new
                        {
                            Id = new Guid("4cc104c7-25ea-4e29-8ca4-b061c1c62880"),
                            Damage = 30,
                            Defence = 0,
                            Name = "Diamond Sword",
                            Type = "Weapon"
                        },
                        new
                        {
                            Id = new Guid("84405d79-bcfc-4dd6-ae47-de1aee663791"),
                            Damage = 0,
                            Defence = 5,
                            Name = "Basic Helmet",
                            Type = "Helmet"
                        },
                        new
                        {
                            Id = new Guid("8dfe2d2e-c7ff-4cbd-8340-77a0bbf68fad"),
                            Damage = 0,
                            Defence = 10,
                            Name = "Leather Helmet",
                            Type = "Helmet"
                        },
                        new
                        {
                            Id = new Guid("e0205c66-f774-46dc-a096-080f8bd8cd5e"),
                            Damage = 0,
                            Defence = 15,
                            Name = "Iron Helmet",
                            Type = "Helmet"
                        },
                        new
                        {
                            Id = new Guid("79f1f26f-abb3-47e1-bf4c-398f9247bb3a"),
                            Damage = 0,
                            Defence = 20,
                            Name = "Steel Helmet",
                            Type = "Helmet"
                        },
                        new
                        {
                            Id = new Guid("26e626de-d534-460b-b340-c27786f77cad"),
                            Damage = 0,
                            Defence = 25,
                            Name = "Titanium Helmet",
                            Type = "Helmet"
                        },
                        new
                        {
                            Id = new Guid("f4d991ed-cf24-41cd-8d7a-6561797976ee"),
                            Damage = 0,
                            Defence = 10,
                            Name = "Basic Armor",
                            Type = "Body Armor"
                        },
                        new
                        {
                            Id = new Guid("a52ee975-52b6-46be-8b27-093a4be5ba7a"),
                            Damage = 0,
                            Defence = 20,
                            Name = "Leather Armor",
                            Type = "Body Armor"
                        },
                        new
                        {
                            Id = new Guid("a6043ad9-7e4a-4548-9932-8d6068c75ef8"),
                            Damage = 0,
                            Defence = 30,
                            Name = "Iron Armor",
                            Type = "Body Armor"
                        },
                        new
                        {
                            Id = new Guid("5c919272-ba64-4bdc-9dbd-a20c08b5d190"),
                            Damage = 0,
                            Defence = 40,
                            Name = "Steel Armor",
                            Type = "Body Armor"
                        },
                        new
                        {
                            Id = new Guid("16dcd5a3-af03-4235-a892-a306a9f00e40"),
                            Damage = 0,
                            Defence = 50,
                            Name = "Titanium Armor",
                            Type = "Body Armor"
                        });
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Monster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("Defence")
                        .HasColumnType("integer");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Monsters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed660b52-a502-43ce-9549-c6e5d6ed8072"),
                            Damage = 5,
                            Defence = 1,
                            Health = 30,
                            Level = 1,
                            Name = "Witch"
                        },
                        new
                        {
                            Id = new Guid("00b39ffb-4694-420a-bd2e-7b9c932d3d4d"),
                            Damage = 10,
                            Defence = 5,
                            Health = 50,
                            Level = 5,
                            Name = "Deadeye"
                        },
                        new
                        {
                            Id = new Guid("2c0f2fc1-c93c-4da1-b7b5-687b823be618"),
                            Damage = 20,
                            Defence = 10,
                            Health = 70,
                            Level = 10,
                            Name = "Assassin"
                        },
                        new
                        {
                            Id = new Guid("54daf5fb-2c39-4931-a8a6-f136ac2c645b"),
                            Damage = 30,
                            Defence = 15,
                            Health = 75,
                            Level = 15,
                            Name = "Occultist"
                        },
                        new
                        {
                            Id = new Guid("323ba06b-2ec1-4609-9f23-9d40626d4424"),
                            Damage = 40,
                            Defence = 30,
                            Health = 150,
                            Level = 20,
                            Name = "Elementalist"
                        },
                        new
                        {
                            Id = new Guid("c6b08855-2d05-47a1-b369-54e045822f65"),
                            Damage = 100,
                            Defence = 50,
                            Health = 300,
                            Level = 25,
                            Name = "Juggernaut"
                        });
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BackpackId")
                        .HasColumnType("uuid");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("Defence")
                        .HasColumnType("integer");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("Experience")
                        .HasColumnType("integer");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dd65bd59-a483-485f-adde-4eb624ddffc4"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4"),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9855d8e0-1756-4ae4-a6aa-8822c9821d5f"),
                            Email = "admin@admin.com",
                            Name = "Admin",
                            Password = "$2a$10$bpdj5yy6B7q.FkwWkTL9QO.mA/o/xUEJw5xPOqcwZr6rcwybuxHqe",
                            RoleId = new Guid("dd65bd59-a483-485f-adde-4eb624ddffc4")
                        },
                        new
                        {
                            Id = new Guid("2d172af7-5f88-4edc-9242-155ec2fc3d75"),
                            Email = "john@doe.com",
                            Name = "John",
                            Password = "$2a$10$W9uxDkyq2GrVG3EcKJfqw.zDW2Ouip2qGGWIEZ4acMcXGWF/9g2LO",
                            RoleId = new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4")
                        },
                        new
                        {
                            Id = new Guid("211a97a6-f3c1-4544-9fc1-fc7c07ff3af3"),
                            Email = "chris@wilson.com",
                            Name = "Chris",
                            Password = "$2a$10$sTiKenzm52qe9n18HZg23e/WCfA6HAYSR5eeWJKMkDHIswsdL9VUm",
                            RoleId = new Guid("19e0ab0c-8bba-4d4d-a3d2-c5fc258aa8a4")
                        });
                });

            modelBuilder.Entity("EquipmentItem", b =>
                {
                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.HasKey("EquipmentId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("EquipmentItem");
                });

            modelBuilder.Entity("BackpackItem", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Backpack", null)
                        .WithMany()
                        .HasForeignKey("BackpackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Backpack", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Player", "Player")
                        .WithOne("Backpack")
                        .HasForeignKey("DungeonsAndExiles.Api.Models.Domain.Backpack", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Equipment", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Player", "Player")
                        .WithOne("Equipment")
                        .HasForeignKey("DungeonsAndExiles.Api.Models.Domain.Equipment", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Player", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.User", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EquipmentItem", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Equipment", null)
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Player", b =>
                {
                    b.Navigation("Backpack")
                        .IsRequired();

                    b.Navigation("Equipment")
                        .IsRequired();
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.User", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}