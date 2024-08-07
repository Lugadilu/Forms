﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FormAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FormAPI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240520172827_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "hstore");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FormAPI.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("forms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Ask some personal questions for statistics",
                            Name = "Questionnaire"
                        });
                });

            modelBuilder.Entity("FormAPI.Models.FormField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Dictionary<string, string>>("Attributes")
                        .IsRequired()
                        .HasColumnType("hstore");

                    b.Property<string>("FieldType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Required")
                        .HasColumnType("boolean");

                    b.Property<Dictionary<string, string>>("Rules")
                        .IsRequired()
                        .HasColumnType("hstore");

                    b.HasKey("Id");

                    b.ToTable("formfields");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Attributes = new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" },
                            FieldType = "text",
                            Kind = "profile",
                            Name = "first name",
                            Required = true,
                            Rules = new Dictionary<string, string> { ["MinLength"] = "5", ["MaxLength"] = "20", ["RegexPattern"] = "^\\w+$" }
                        });
                });

            modelBuilder.Entity("FormAPI.Models.FormRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Dictionary<string, string>>("Attributes")
                        .IsRequired()
                        .HasColumnType("hstore");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FieldType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nationality")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Zip")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("formrecords");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Address = "123 Main St",
                            Arrival = new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc),
                            Attributes = new Dictionary<string, string> { ["Attribute1"] = "Value1", ["Attribute2"] = "Value2" },
                            Birthdate = new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = "Philadephia",
                            Country = "USA",
                            Departure = new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "jane@example.com",
                            FieldType = "text",
                            FirstName = "Jane",
                            Gender = "Female",
                            Kind = "profile",
                            LastName = "Doe",
                            PhoneNumber = "0714665512",
                            SecondName = "Doe",
                            Zip = "12345"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
