﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZPDSGGW.Database;

namespace ZPDSGGW.Migrations
{
    [DbContext(typeof(ZPDSGGWContext))]
    partial class ZPDSGGWContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ZPDSGGW.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<int>("DocumentKind")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("ZPDSGGW.Models.InvitationPromoter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PromoterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromoterId");

                    b.HasIndex("StudentId");

                    b.ToTable("InvitationPromoter");
                });

            modelBuilder.Entity("ZPDSGGW.Models.Promoter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<int>("Degrees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promoter");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2f6784c-cac2-4949-b81d-63584314d797"),
                            Availability = 0,
                            Degrees = 3,
                            Name = "Promotor1",
                            Surname = "Test"
                        },
                        new
                        {
                            Id = new Guid("3b00f1d2-f701-4b63-9a22-5c3d602bf7d2"),
                            Availability = 0,
                            Degrees = 7,
                            Name = "Promotor2",
                            Surname = "Test"
                        },
                        new
                        {
                            Id = new Guid("5a1298d3-c95b-4f0e-96d9-1b1881653777"),
                            Availability = 0,
                            Degrees = 4,
                            Name = "Promotor3",
                            Surname = "Test"
                        });
                });

            modelBuilder.Entity("ZPDSGGW.Models.Proposal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromoterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromoterSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Proposal");
                });

            modelBuilder.Entity("ZPDSGGW.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("ZPDSGGW.Models.ThesisTopic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PromoterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromoterId");

                    b.ToTable("ThesisTopics");
                });

            modelBuilder.Entity("ZPDSGGW.Models.InvitationPromoter", b =>
                {
                    b.HasOne("ZPDSGGW.Models.Promoter", "Promoter")
                        .WithMany()
                        .HasForeignKey("PromoterId");

                    b.HasOne("ZPDSGGW.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Promoter");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ZPDSGGW.Models.ThesisTopic", b =>
                {
                    b.HasOne("ZPDSGGW.Models.Promoter", "Promoter")
                        .WithMany()
                        .HasForeignKey("PromoterId");

                    b.Navigation("Promoter");
                });
#pragma warning restore 612, 618
        }
    }
}
