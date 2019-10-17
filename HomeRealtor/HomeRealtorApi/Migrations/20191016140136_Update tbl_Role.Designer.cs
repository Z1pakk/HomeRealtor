﻿// <auto-generated />
using System;
using HomeRealtorApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeRealtorApi.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20191016140136_Update tbl_Role")]
    partial class Updatetbl_Role
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeRealtorApi.Entities.Advertising", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacts")
                        .IsRequired();

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<string>("StateName")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_Advertisings");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.ImageEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EstateId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EstateId");

                    b.ToTable("tbl_ImageEstates");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.ImageUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_ImageUsers");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Headline")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("tbl.News");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartId");

                    b.Property<int>("RealtorId");

                    b.Property<bool>("Status");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApartId");

                    b.HasIndex("RealtorId");

                    b.HasIndex("UserId");

                    b.ToTable("tblOrders");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<double>("Price");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<double>("TerritorySize");

                    b.Property<DateTime>("TimeOfPost");

                    b.Property<int>("TypeId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_RealStates");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.RealEstateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("tblEstateTypes");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("tbl_Roles");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe")
                        .HasMaxLength(100);

                    b.Property<int>("Age");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Image");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("tbl_Users");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.UserRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_UserRoles");
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.Advertising", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.User", "UserOf")
                        .WithMany("Advertisings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.ImageEstate", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.RealEstate", "EstateOf")
                        .WithMany("ImageEstates")
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.ImageUser", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.User", "UserOf")
                        .WithMany("ImageUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.Order", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.RealEstate", "EstateOf")
                        .WithMany()
                        .HasForeignKey("ApartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeRealtorApi.Entities.User", "RealtorOf")
                        .WithMany()
                        .HasForeignKey("RealtorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeRealtorApi.Entities.User", "UserOf")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.RealEstate", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.RealEstateType", "TypeOf")
                        .WithMany("RealEstates")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeRealtorApi.Entities.User", "UserOf")
                        .WithMany("RealEstates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeRealtorApi.Entities.UserRole", b =>
                {
                    b.HasOne("HomeRealtorApi.Entities.Role", "RoleOf")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeRealtorApi.Entities.User", "UserOf")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
