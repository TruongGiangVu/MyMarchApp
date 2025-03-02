﻿// <auto-generated />
using System;
using MarchApi.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MarchApi.Migrations
{
    [DbContext(typeof(MarchContext))]
    [Migration("20250302055120_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.13");

            modelBuilder.Entity("MarchApi.Models.DbUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("DbUsers");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoCheckList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ToDoItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ToDoItemId");

                    b.ToTable("ToDoCheckLists");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Rate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDoTags");
                });

            modelBuilder.Entity("ToDoTagItem", b =>
                {
                    b.Property<string>("ToDoItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ToDoTagId")
                        .HasColumnType("TEXT");

                    b.HasKey("ToDoItemId", "ToDoTagId");

                    b.HasIndex("ToDoTagId");

                    b.ToTable("ToDoTagItem");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoCheckList", b =>
                {
                    b.HasOne("MarchApi.Models.ToDoItem", "ToDoItem")
                        .WithMany("ToDoCheckLists")
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ToDoItem");
                });

            modelBuilder.Entity("ToDoTagItem", b =>
                {
                    b.HasOne("MarchApi.Models.ToDoItem", null)
                        .WithMany()
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarchApi.Models.ToDoTag", null)
                        .WithMany()
                        .HasForeignKey("ToDoTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarchApi.Models.ToDoItem", b =>
                {
                    b.Navigation("ToDoCheckLists");
                });
#pragma warning restore 612, 618
        }
    }
}
