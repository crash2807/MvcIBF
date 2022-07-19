﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcIBF.Data;

#nullable disable

namespace MvcIBF.Migrations
{
    [DbContext(typeof(MvcIBFContext))]
    [Migration("20220711193416_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieVOD", b =>
                {
                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("int");

                    b.Property<int>("VODsVodId")
                        .HasColumnType("int");

                    b.HasKey("MoviesMovieId", "VODsVodId");

                    b.HasIndex("VODsVodId");

                    b.ToTable("MovieVOD");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<string>("MovieDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Runtime")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.ToTable("Movie", (string)null);
                });

            modelBuilder.Entity("MvcIBF.Models.VOD", b =>
                {
                    b.Property<int>("VodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VodId"), 1L, 1);

                    b.Property<string>("VodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VodId");

                    b.ToTable("VODs");
                });

            modelBuilder.Entity("MovieVOD", b =>
                {
                    b.HasOne("MvcIBF.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcIBF.Models.VOD", null)
                        .WithMany()
                        .HasForeignKey("VODsVodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}