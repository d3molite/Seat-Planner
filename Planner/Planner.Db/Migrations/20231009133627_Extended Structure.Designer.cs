﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planner.Db.Context;

#nullable disable

namespace Planner.Db.Migrations
{
    [DbContext(typeof(PlannerDbContext))]
    [Migration("20231009133627_Extended Structure")]
    partial class ExtendedStructure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Planner.Db.Models.Attendee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SeatIdentifier")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("Planner.Db.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Planner.Db.Models.SeatingRow", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("EventId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StartingSeat")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("Planner.Db.Models.Attendee", b =>
                {
                    b.HasOne("Planner.Db.Models.Event", null)
                        .WithMany("Attendees")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("Planner.Db.Models.SeatingRow", b =>
                {
                    b.HasOne("Planner.Db.Models.Event", null)
                        .WithMany("SeatingRows")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("Planner.Db.Models.Event", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("SeatingRows");
                });
#pragma warning restore 612, 618
        }
    }
}
