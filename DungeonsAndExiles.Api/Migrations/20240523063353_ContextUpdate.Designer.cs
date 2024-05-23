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
    [Migration("20240523063353_ContextUpdate")]
    partial class ContextUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<Guid>("BackpackId")
                        .HasColumnType("uuid");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("Defence")
                        .HasColumnType("integer");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BackpackId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Items");
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

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cd0897c1-f361-4685-bd00-c3d56a0c8290"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0"),
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
                            Id = new Guid("ec997402-6b71-4112-a8b4-a3194ebabd0d"),
                            Email = "admin@admin.com",
                            Name = "Admin",
                            Password = "$2a$10$T/EDg0HHUC3ZZxb0WpDYjuSvdtOkzJYqxVJ4oAebI1US7PWmGS0V.",
                            RoleId = new Guid("cd0897c1-f361-4685-bd00-c3d56a0c8290")
                        },
                        new
                        {
                            Id = new Guid("5556db48-1b6d-462a-93ab-fb7a12209fd3"),
                            Email = "john@doe.com",
                            Name = "John",
                            Password = "$2a$10$ZBTfzq4rzBIdZQW4Af4XPeNwHn6FxRQknLH5/mP3s6v9.w7O2ykvW",
                            RoleId = new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0")
                        },
                        new
                        {
                            Id = new Guid("28649b79-c8ac-4b9f-909b-1f01cbd1174f"),
                            Email = "chris@wilson.com",
                            Name = "Chris",
                            Password = "$2a$10$O20fOAjEjgh3Fr.WEJXy5OR1dBUrtJCRRpLhzUMLby0UdaU.27EgO",
                            RoleId = new Guid("d042eba2-82a8-4c79-9bc9-27a03a3a64c0")
                        });
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

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Item", b =>
                {
                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Backpack", "Backpack")
                        .WithMany("Items")
                        .HasForeignKey("BackpackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Equipment", "Equipment")
                        .WithMany("Items")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DungeonsAndExiles.Api.Models.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Backpack");

                    b.Navigation("Equipment");

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
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Backpack", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Equipment", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Player", b =>
                {
                    b.Navigation("Backpack")
                        .IsRequired();

                    b.Navigation("Equipment")
                        .IsRequired();
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.User", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
