﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(PIMDatabaseContext))]
    [Migration("20241106235306_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.Property<string>("Visa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("LeaderId")
                        .HasColumnType("bigint");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<long?>("GroupId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("ProjectNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2y$10$uFZ64K25vYOZBOzoIquHGuK8Sea4Nhlllg2JW53T04Owp3r0sc0re",
                            Role = 0,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "$2y$10$uFZ64K25vYOZBOzoIquHGuK8Sea4Nhlllg2JW53T04Owp3r0sc0re",
                            Role = 1,
                            Username = "jane"
                        });
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.Property<long>("EmployeesId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectsId")
                        .HasColumnType("bigint");

                    b.HasKey("EmployeesId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Group", b =>
                {
                    b.HasOne("DataAccessLayer.BusinessObject.Employee", "Leader")
                        .WithMany("Groups")
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Project", b =>
                {
                    b.HasOne("DataAccessLayer.BusinessObject.Group", "GroupProject")
                        .WithOne("Project")
                        .HasForeignKey("DataAccessLayer.BusinessObject.Project", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupProject");
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.HasOne("DataAccessLayer.BusinessObject.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.BusinessObject.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Employee", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("DataAccessLayer.BusinessObject.Group", b =>
                {
                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
