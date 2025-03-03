﻿// <auto-generated />
using System;
using Flush_It_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flush_It_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231115043144_Users")]
    partial class Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Flush_It_API.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Alcohol")
                        .HasColumnType("boolean");

                    b.Property<bool?>("ArtificialAdditives")
                        .HasColumnType("boolean");

                    b.Property<bool?>("Bread")
                        .HasColumnType("boolean");

                    b.Property<bool?>("Caffeine")
                        .HasColumnType("boolean");

                    b.Property<double?>("Calcium")
                        .HasColumnType("double precision");

                    b.Property<int?>("Calories")
                        .HasColumnType("integer");

                    b.Property<bool?>("CarbonatedBeverage")
                        .HasColumnType("boolean");

                    b.Property<double?>("Carbs")
                        .HasColumnType("double precision");

                    b.Property<double?>("Cholesterol")
                        .HasColumnType("double precision");

                    b.Property<bool?>("DairyProduct")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool?>("FODMAP")
                        .HasColumnType("boolean");

                    b.Property<double?>("Fiber")
                        .HasColumnType("double precision");

                    b.Property<bool?>("Fried")
                        .HasColumnType("boolean");

                    b.Property<bool?>("Gluten")
                        .HasColumnType("boolean");

                    b.Property<double?>("Iron")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Potassium")
                        .HasColumnType("double precision");

                    b.Property<bool?>("Processed")
                        .HasColumnType("boolean");

                    b.Property<double?>("Protein")
                        .HasColumnType("double precision");

                    b.Property<double?>("SaturatedFat")
                        .HasColumnType("double precision");

                    b.Property<double?>("Sodium")
                        .HasColumnType("double precision");

                    b.Property<bool?>("Spicy")
                        .HasColumnType("boolean");

                    b.Property<double?>("Sugar")
                        .HasColumnType("double precision");

                    b.Property<bool?>("Sweetener")
                        .HasColumnType("boolean");

                    b.Property<double?>("TotalFat")
                        .HasColumnType("double precision");

                    b.Property<double?>("TransFat")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("Flush_It_API.Models.IbsCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("IbsCount");
                });

            modelBuilder.Entity("Flush_It_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
