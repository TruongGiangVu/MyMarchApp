﻿// <auto-generated />
using System;
using MarchApi.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace MarchApi.Migrations
{
    [DbContext(typeof(MarchContext))]
    partial class MarchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MarchApi.Models.DbUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("USER_ID");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("ROLE");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("USER_NAME");

                    b.HasKey("UserId");

                    b.ToTable("DB_USER");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoCheckList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("ID");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("IsDone")
                        .IsRequired()
                        .HasColumnType("VARCHAR2(5)")
                        .HasColumnName("IS_DONE");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("TEXT");

                    b.Property<string>("ToDoItemId")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("ITEM_ID");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("UPDATED_BY");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("UPDATED_TIME");

                    b.HasKey("Id");

                    b.HasIndex("ToDoItemId");

                    b.ToTable("TODO_CHECKLIST");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("ID");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("IsDone")
                        .IsRequired()
                        .HasColumnType("VARCHAR2(5)")
                        .HasColumnName("IS_DONE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NAME");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("VARCHAR2(20)")
                        .HasColumnName("PRIORITY");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("NUMBER")
                        .HasColumnName("RATE");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("UPDATED_BY");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("UPDATED_TIME");

                    b.HasKey("Id");

                    b.ToTable("TODO_ITEM");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("ID");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("COLOR");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("UPDATED_BY");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("DATE")
                        .HasColumnName("UPDATED_TIME");

                    b.HasKey("Id");

                    b.ToTable("TODO_TAG");
                });

            modelBuilder.Entity("TODO_TAG_ITEM", b =>
                {
                    b.Property<string>("ITEM_ID")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("TAG_ID")
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("ITEM_ID", "TAG_ID");

                    b.HasIndex("TAG_ID");

                    b.ToTable("TODO_TAG_ITEM");
                });

            modelBuilder.Entity("MarchApi.Models.ToDoCheckList", b =>
                {
                    b.HasOne("MarchApi.Models.ToDoItem", "ToDoItem")
                        .WithMany("ToDoCheckLists")
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ToDoItem");
                });

            modelBuilder.Entity("TODO_TAG_ITEM", b =>
                {
                    b.HasOne("MarchApi.Models.ToDoItem", null)
                        .WithMany()
                        .HasForeignKey("ITEM_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarchApi.Models.ToDoTag", null)
                        .WithMany()
                        .HasForeignKey("TAG_ID")
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
