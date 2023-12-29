﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(HRContext))]
    [Migration("20231228193356_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AppProficiency")
                        .HasColumnType("text")
                        .HasColumnName("app_proficiency");

                    b.Property<string>("Birthdate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("birthdate");

                    b.Property<string>("EnglishLevel")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("english_level");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("Institution")
                        .HasColumnType("text")
                        .HasColumnName("institution");

                    b.Property<bool?>("IsStudent")
                        .IsRequired()
                        .HasColumnType("boolean")
                        .HasColumnName("is_student");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("media");

                    b.Property<bool?>("NightShift")
                        .IsRequired()
                        .HasColumnType("boolean")
                        .HasColumnName("night_shift");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("register_time");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("resume");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("source");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("StudyForm")
                        .HasColumnType("text")
                        .HasColumnName("study_form");

                    b.Property<string>("VacancyType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("vacancy_type");

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.ToTable("employees", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
