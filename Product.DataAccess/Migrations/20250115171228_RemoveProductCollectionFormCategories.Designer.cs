﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductHub.DataAccess;
using ProductHub.DataAccess.Data;

#nullable disable

namespace ProductHub.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250115171228_RemoveProductCollectionFormCategories")]
    partial class RemoveProductCollectionFormCategories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("ProductHub.DataAccess.Entities.Product", b =>
                {
                    b.HasOne("ProductHub.DataAccess.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
