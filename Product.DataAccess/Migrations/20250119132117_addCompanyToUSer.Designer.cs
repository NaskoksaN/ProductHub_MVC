﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductHub.DataAccess.Data;

#nullable disable

namespace ProductHub.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250119132117_addCompanyToUSer")]
    partial class addCompanyToUSer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int")
                        .HasComment("Display Order");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Category Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Soap"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Shower Gel"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Premium Soap"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Shampoo"
                        });
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Company Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("Company City");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Company Name");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Company Phone Number");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Company Postal Code");

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Company Street Address");

                    b.Property<string>("VAT")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Company VAT");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Varna",
                            Name = "Nasko Bobchev Ltd",
                            PhoneNumber = "0012312334",
                            PostalCode = "BG 9000",
                            StreetAddress = "jk Pobeda, str Todor radev Penev 7 - ap16",
                            VAT = "BG41023450"
                        },
                        new
                        {
                            Id = 2,
                            City = "Haskovo",
                            Name = "Nasko  Ltd",
                            PhoneNumber = "0023312334",
                            PostalCode = "BG 6300",
                            StreetAddress = "str Gurgulqt 2, ent.B , app58",
                            VAT = "BG41023123"
                        },
                        new
                        {
                            Id = 3,
                            City = "selo Kukovica",
                            Name = "Mega company Ltd",
                            PhoneNumber = "01223312334",
                            PostalCode = "BG 1300",
                            StreetAddress = "str GPetko petkov 12",
                            VAT = "BG41034123"
                        });
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasComment("Product amount");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasComment("Product Description");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("int")
                        .HasComment("Unit of measurement");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Product name");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasComment("Product price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 120,
                            CategoryId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "Soap1",
                            Price = 1.24
                        },
                        new
                        {
                            Id = 2,
                            Amount = 150,
                            CategoryId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "Soap2",
                            Price = 1.3999999999999999
                        },
                        new
                        {
                            Id = 3,
                            Amount = 90,
                            CategoryId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "Soap3",
                            Price = 0.89000000000000001
                        },
                        new
                        {
                            Id = 4,
                            Amount = 140,
                            CategoryId = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "Soap4",
                            Price = 1.3
                        },
                        new
                        {
                            Id = 5,
                            Amount = 70,
                            CategoryId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "PremiumSoap1",
                            Price = 5.2400000000000002
                        },
                        new
                        {
                            Id = 6,
                            Amount = 60,
                            CategoryId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "PremiumSoap2",
                            Price = 4.2400000000000002
                        },
                        new
                        {
                            Id = 7,
                            Amount = 90,
                            CategoryId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "PremiumSoap3",
                            Price = 9.2400000000000002
                        },
                        new
                        {
                            Id = 8,
                            Amount = 40,
                            CategoryId = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 0,
                            Name = "PremiumSoap4",
                            Price = 11.24
                        },
                        new
                        {
                            Id = 9,
                            Amount = 550,
                            CategoryId = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 1,
                            Name = "ShowerGel1",
                            Price = 15.24
                        },
                        new
                        {
                            Id = 10,
                            Amount = 400,
                            CategoryId = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 1,
                            Name = "ShowerGel2",
                            Price = 21.239999999999998
                        },
                        new
                        {
                            Id = 11,
                            Amount = 400,
                            CategoryId = 4,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                            ImgUrl = "",
                            MeasurementUnit = 1,
                            Name = "Shampoo",
                            Price = 112.23999999999999
                        });
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasComment("User city");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("User full name");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("User postal code");

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("User street address");

                    b.HasIndex("CompanyId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.Product", b =>
                {
                    b.HasOne("ProductHub.DataAccess.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProductHub.DataAccess.Entities.ApplicationUser", b =>
                {
                    b.HasOne("ProductHub.DataAccess.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
