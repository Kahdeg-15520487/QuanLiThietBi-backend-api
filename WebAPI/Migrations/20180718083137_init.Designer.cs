﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180718083137_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebAPI.Models.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DeviceDetailId");

                    b.Property<string>("Note");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("DeviceDetailId");

                    b.ToTable("Devices");

                    b.HasData(
                        new { Id = new Guid("9d83072a-f7b3-42af-8c95-fdad77e01600"), DeviceDetailId = new Guid("ee7df131-a399-40b4-8ed7-5777aeca1297"), Quantity = 10 },
                        new { Id = new Guid("591f5b7a-9295-4b27-8764-1734ab140b77"), DeviceDetailId = new Guid("dc201ae7-b1c6-4ab4-ae25-e4d692d93051"), Quantity = 10 },
                        new { Id = new Guid("489f4306-3f08-421b-af93-ce735daa3f1a"), DeviceDetailId = new Guid("b8a15f7e-086b-42a7-a576-806bf8a6766b"), Quantity = 10 },
                        new { Id = new Guid("ea952c8b-da8c-4f07-83e8-6ef12ebc55d2"), DeviceDetailId = new Guid("1bced5bd-d043-4076-b114-32c8b9b55be9"), Quantity = 10 },
                        new { Id = new Guid("36d6e1ba-a3e0-47b5-a572-456a096d2fcb"), DeviceDetailId = new Guid("5ff00880-153f-4006-b29f-6926f8240410"), Quantity = 10 },
                        new { Id = new Guid("fd834666-daa9-4de8-946b-041fd42b4c75"), DeviceDetailId = new Guid("dbe83347-aebc-4bb1-96dc-344cd697296c"), Quantity = 10 }
                    );
                });

            modelBuilder.Entity("WebAPI.Models.DeviceCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("Id");

                    b.ToTable("DeviceCategorys");

                    b.HasData(
                        new { Id = new Guid("c9518049-617c-4297-8fc1-e2cb8d74f155"), CategoryName = "Monitor" },
                        new { Id = new Guid("e739e073-2022-4e6e-9972-3c5534403481"), CategoryName = "Keyboard" },
                        new { Id = new Guid("d15eab93-acb5-4dfc-b449-ccd99adcc389"), CategoryName = "Computer" },
                        new { Id = new Guid("25ca7f9f-8fe3-43d3-8823-2ec7c3336c1d"), CategoryName = "VGA cable" },
                        new { Id = new Guid("564c74d8-d914-4ef6-b701-200d8d56c83d"), CategoryName = "Power cable" }
                    );
                });

            modelBuilder.Entity("WebAPI.Models.DeviceDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DeviceCategoryId");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("DeviceCategoryId");

                    b.ToTable("DeviceDetails");

                    b.HasData(
                        new { Id = new Guid("ee7df131-a399-40b4-8ed7-5777aeca1297"), DeviceCategoryId = new Guid("c9518049-617c-4297-8fc1-e2cb8d74f155"), Name = "Dell Monitor" },
                        new { Id = new Guid("dc201ae7-b1c6-4ab4-ae25-e4d692d93051"), DeviceCategoryId = new Guid("c9518049-617c-4297-8fc1-e2cb8d74f155"), Name = "Dell Wide Monitor" },
                        new { Id = new Guid("b8a15f7e-086b-42a7-a576-806bf8a6766b"), DeviceCategoryId = new Guid("e739e073-2022-4e6e-9972-3c5534403481"), Name = "Logitech K120 Keyboard" },
                        new { Id = new Guid("1bced5bd-d043-4076-b114-32c8b9b55be9"), DeviceCategoryId = new Guid("d15eab93-acb5-4dfc-b449-ccd99adcc389"), Name = "Dell OEM Computer" },
                        new { Id = new Guid("5ff00880-153f-4006-b29f-6926f8240410"), DeviceCategoryId = new Guid("25ca7f9f-8fe3-43d3-8823-2ec7c3336c1d"), Name = "VGA cable" },
                        new { Id = new Guid("dbe83347-aebc-4bb1-96dc-344cd697296c"), DeviceCategoryId = new Guid("564c74d8-d914-4ef6-b701-200d8d56c83d"), Name = "Power cable" }
                    );
                });

            modelBuilder.Entity("WebAPI.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Expire");

                    b.Property<string>("Token");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WebAPI.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("DeviceId");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("Priority");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("WebAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebAPI.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebAPI.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebAPI.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Device", b =>
                {
                    b.HasOne("WebAPI.Models.DeviceDetail", "DeviceDetail")
                        .WithMany()
                        .HasForeignKey("DeviceDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.DeviceDetail", b =>
                {
                    b.HasOne("WebAPI.Models.DeviceCategory", "DeviceCategory")
                        .WithMany()
                        .HasForeignKey("DeviceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.RefreshToken", b =>
                {
                    b.HasOne("WebAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebAPI.Models.Ticket", b =>
                {
                    b.HasOne("WebAPI.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
