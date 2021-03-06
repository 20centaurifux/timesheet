﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using timesheet_api.Data;

namespace timesheetapi.Migrations
{
    [DbContext(typeof(TimesheetContext))]
    partial class TimesheetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("timesheet_api.Models.Entry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<long?>("ProjectId");

                    b.Property<int>("Seconds");

                    b.Property<long?>("TaskId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("timesheet_api.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("GroupId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("timesheet_api.Models.ProjectGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("ProjectGroups");
                });

            modelBuilder.Entity("timesheet_api.Models.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<bool>("Productive");

                    b.Property<long?>("ProjectGroupId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectGroupId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("timesheet_api.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("timesheet_api.Models.Entry", b =>
                {
                    b.HasOne("timesheet_api.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("timesheet_api.Models.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.HasOne("timesheet_api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("timesheet_api.Models.Project", b =>
                {
                    b.HasOne("timesheet_api.Models.ProjectGroup", "Group")
                        .WithMany("Projects")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("timesheet_api.Models.Task", b =>
                {
                    b.HasOne("timesheet_api.Models.ProjectGroup", "ProjectGroup")
                        .WithMany()
                        .HasForeignKey("ProjectGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
