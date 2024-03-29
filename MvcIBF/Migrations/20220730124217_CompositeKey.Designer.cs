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
    [Migration("20220730124217_CompositeKey")]
    partial class CompositeKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_VOD", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("VODId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "VODId");

                    b.HasIndex("VODId");

                    b.ToTable("Movies_VODs");
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

            modelBuilder.Entity("MvcIBF.Models.Movie_VOD", b =>
                {
                    b.HasOne("MvcIBF.Models.Movie", "Movie")
                        .WithMany("Movie_VODs")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcIBF.Models.VOD", "VOD")
                        .WithMany("Movie_VODs")
                        .HasForeignKey("VODId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("VOD");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie", b =>
                {
                    b.Navigation("Movie_VODs");
                });

            modelBuilder.Entity("MvcIBF.Models.VOD", b =>
                {
                    b.Navigation("Movie_VODs");
                });
#pragma warning restore 612, 618
        }
    }
}
