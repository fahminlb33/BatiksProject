﻿// <auto-generated />
using BatiksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BatiksProject.Migrations
{
    [DbContext(typeof(BatikContext))]
    partial class BatikContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BatiksProject.Models.Batik", b =>
                {
                    b.Property<int>("BatikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalityName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MinioObjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BatikId");

                    b.HasIndex("LocalityName");

                    b.ToTable("Batiks");
                });

            modelBuilder.Entity("BatiksProject.Models.Locality", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Localities");
                });

            modelBuilder.Entity("BatiksProject.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BatiksProject.Models.Batik", b =>
                {
                    b.HasOne("BatiksProject.Models.Locality", "Locality")
                        .WithMany("Batiks")
                        .HasForeignKey("LocalityName");
                });
#pragma warning restore 612, 618
        }
    }
}