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
    [Migration("20240522170731_Initial-Migration")]
    partial class InitialMigration
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

                    b.HasKey("Id");

                    b.ToTable("Users");
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

            modelBuilder.Entity("DungeonsAndExiles.Api.Models.Domain.User", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
