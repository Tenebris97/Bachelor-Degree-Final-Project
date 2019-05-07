﻿// <auto-generated />
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FinalProject.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190305074437_AddColorWarranty")]
    partial class AddColorWarranty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalProject.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

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

            modelBuilder.Entity("FinalProject.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<byte>("Gender");

                    b.Property<string>("LastName");

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

            modelBuilder.Entity("FinalProject.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("FinalProject.Models.Cellphone", b =>
                {
                    b.Property<int>("CellphoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CameraHas");

                    b.Property<string>("CameraRecording");

                    b.Property<string>("CameraResolution");

                    b.Property<string>("ConnectionNetworks");

                    b.Property<string>("ConnectionTechnologies");

                    b.Property<string>("CpuChipset");

                    b.Property<string>("CpuCore");

                    b.Property<string>("CpuFrequency");

                    b.Property<string>("CpuType");

                    b.Property<string>("Description");

                    b.Property<string>("GPU");

                    b.Property<string>("OS");

                    b.Property<string>("OSVersion");

                    b.Property<int>("ProductId");

                    b.Property<int>("RAM");

                    b.Property<string>("ScreenProtector");

                    b.Property<string>("ScreenSize");

                    b.Property<string>("ScreenTechnology");

                    b.Property<string>("ScreenType");

                    b.Property<int>("SimcardCount");

                    b.Property<string>("SimcardDesc");

                    b.Property<string>("Size");

                    b.Property<int>("Storage");

                    b.Property<int>("StorageSupport");

                    b.Property<string>("StorageType");

                    b.Property<int>("Weight");

                    b.HasKey("CellphoneId");

                    b.HasIndex("ProductId");

                    b.ToTable("cellphones");
                });

            modelBuilder.Entity("FinalProject.Models.Laptop", b =>
                {
                    b.Property<int>("LaptopId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatteryType");

                    b.Property<string>("Bluetooth");

                    b.Property<int>("CpuCache");

                    b.Property<string>("CpuFrequency");

                    b.Property<string>("CpuManufactor");

                    b.Property<string>("CpuSeries");

                    b.Property<string>("CpuType");

                    b.Property<string>("Description");

                    b.Property<string>("GPUManufactor");

                    b.Property<string>("GPUModel");

                    b.Property<string>("GPUSize");

                    b.Property<string>("HDMI");

                    b.Property<string>("Modem");

                    b.Property<string>("ODD");

                    b.Property<string>("OS");

                    b.Property<string>("OSVersion");

                    b.Property<int>("ProductId");

                    b.Property<int>("RAM");

                    b.Property<string>("RAMType");

                    b.Property<string>("ScreenSize");

                    b.Property<string>("ScreenTechnology");

                    b.Property<string>("ScreenType");

                    b.Property<string>("Size");

                    b.Property<string>("Speaker");

                    b.Property<string>("Storage");

                    b.Property<string>("StorageType");

                    b.Property<string>("USB2");

                    b.Property<string>("USB3");

                    b.Property<string>("VGA");

                    b.Property<string>("Webcam");

                    b.Property<int>("Weight");

                    b.Property<string>("Wifi");

                    b.HasKey("LaptopId");

                    b.HasIndex("ProductId");

                    b.ToTable("laptops");
                });

            modelBuilder.Entity("FinalProject.Models.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NewsContent")
                        .IsRequired();

                    b.Property<string>("NewsDate");

                    b.Property<string>("NewsImage");

                    b.Property<string>("NewsTitle")
                        .IsRequired();

                    b.HasKey("NewsId");

                    b.ToTable("news");
                });

            modelBuilder.Entity("FinalProject.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("ProductBrand");

                    b.Property<string>("ProductColor");

                    b.Property<string>("ProductDescription");

                    b.Property<int>("ProductDiscount");

                    b.Property<string>("ProductImage");

                    b.Property<int>("ProductLikeCount");

                    b.Property<string>("ProductName");

                    b.Property<int>("ProductPrice");

                    b.Property<int>("ProductStock");

                    b.Property<int>("ProductViews");

                    b.Property<string>("ProductWarranty");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("FinalProject.Models.Cellphone", b =>
                {
                    b.HasOne("FinalProject.Models.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.Laptop", b =>
                {
                    b.HasOne("FinalProject.Models.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.Product", b =>
                {
                    b.HasOne("FinalProject.Models.Category", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("FinalProject.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FinalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FinalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("FinalProject.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FinalProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
