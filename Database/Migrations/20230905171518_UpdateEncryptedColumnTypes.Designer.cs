﻿// <auto-generated />
using System;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(VaultContext))]
    [Migration("20230905171518_UpdateEncryptedColumnTypes")]
    partial class UpdateEncryptedColumnTypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Email")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Firstname")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Surname")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Models.UserKeyMetadata", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Email")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("IV")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UserKeyMetadata");
                });

            modelBuilder.Entity("Database.Models.Vault", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vaults");
                });

            modelBuilder.Entity("Database.Models.VaultLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Category")
                        .HasColumnType("BLOB");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Description")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Email")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Notes")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("URL")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Username")
                        .HasColumnType("BLOB");

                    b.Property<Guid>("VaultId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VaultId");

                    b.ToTable("VaultLogins");
                });

            modelBuilder.Entity("Database.Models.VaultNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Description")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Note")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VaultId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VaultId");

                    b.ToTable("VaultNotes");
                });

            modelBuilder.Entity("Database.Models.Vault", b =>
                {
                    b.HasOne("Database.Models.User", "User")
                        .WithMany("Vaults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Models.VaultLogin", b =>
                {
                    b.HasOne("Database.Models.Vault", "Vault")
                        .WithMany("Logins")
                        .HasForeignKey("VaultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vault");
                });

            modelBuilder.Entity("Database.Models.VaultNote", b =>
                {
                    b.HasOne("Database.Models.Vault", "Vault")
                        .WithMany("Notes")
                        .HasForeignKey("VaultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vault");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Navigation("Vaults");
                });

            modelBuilder.Entity("Database.Models.Vault", b =>
                {
                    b.Navigation("Logins");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
