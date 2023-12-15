﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SMS.Infrastructure.Data;

#nullable disable

namespace SMS.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20231215070307__course-concurrency-token")]
    partial class _courseconcurrencytoken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SMS.Domain.Aggregates.DepartmentAggregates.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Course", "sms");
                });

            modelBuilder.Entity("SMS.Domain.Aggregates.DepartmentAggregates.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("Name");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Department", "sms");
                });

            modelBuilder.Entity("SMS.Domain.Aggregates.DepartmentAggregates.Course", b =>
                {
                    b.HasOne("SMS.Domain.Aggregates.DepartmentAggregates.Department", null)
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId");

                    b.OwnsOne("SMS.Domain.Aggregates.DepartmentAggregates.CourseInfo", "CourseInfo", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("Code");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("character varying(150)")
                                .HasColumnName("Name");

                            b1.HasKey("CourseId");

                            b1.ToTable("Course", "sms");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("CourseInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("SMS.Domain.Aggregates.DepartmentAggregates.Department", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
