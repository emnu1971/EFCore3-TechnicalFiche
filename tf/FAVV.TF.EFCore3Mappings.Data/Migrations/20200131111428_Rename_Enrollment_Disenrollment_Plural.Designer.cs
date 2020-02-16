﻿// <auto-generated />
using System;
using FAVV.TF.EFCore3Mappings.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FAVV.TF.EFCore3Mappings.Data.Migrations
{
    [DbContext(typeof(CourseManagementContext))]
    [Migration("20200131111428_Rename_Enrollment_Disenrollment_Plural")]
    partial class Rename_Enrollment_Disenrollment_Plural
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long?>("TeachedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("TeachedById");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Disenrollment", b =>
                {
                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Disenrollments");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Enrollment", b =>
                {
                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Course", b =>
                {
                    b.HasOne("FAVV.TF.EFCore3Mappings.Domain.Teacher", "TeachedBy")
                        .WithMany()
                        .HasForeignKey("TeachedById");
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Disenrollment", b =>
                {
                    b.HasOne("FAVV.TF.EFCore3Mappings.Domain.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FAVV.TF.EFCore3Mappings.Domain.Student", "Student")
                        .WithMany("Disenrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FAVV.TF.EFCore3Mappings.Domain.Enrollment", b =>
                {
                    b.HasOne("FAVV.TF.EFCore3Mappings.Domain.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FAVV.TF.EFCore3Mappings.Domain.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
