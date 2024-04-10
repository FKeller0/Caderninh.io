﻿// <auto-generated />
using System;
using Caderninh.io.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Caderninh.io.Infrastructure.Migrations
{
    [DbContext(typeof(CaderninhoDbContext))]
    [Migration("20240410222444_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Caderninh.io.Domain.Notes.NoteCategory", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("_noteIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("NoteIds");

                    b.HasKey("Name");

                    b.HasIndex("Name", "Id")
                        .IsUnique();

                    b.ToTable("NoteCategories");
                });

            modelBuilder.Entity("Caderninh.io.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}