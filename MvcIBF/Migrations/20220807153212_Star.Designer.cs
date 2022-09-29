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
    [Migration("20220807153212_Star")]
    partial class Star
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MvcIBF.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CountryId");

                    b.HasIndex("CountryName")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MvcIBF.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GenreId");

                    b.HasIndex("GenreName")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MvcIBF.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"), 1L, 1);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaterialId");

                    b.HasIndex("MovieId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("MvcIBF.Models.Mood", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MoodId"), 1L, 1);

                    b.Property<string>("MoodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MoodId");

                    b.HasIndex("MoodName")
                        .IsUnique();

                    b.ToTable("Moods");
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

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Country", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("Movie_Countries");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Genre", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movie_Genres");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Mood", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("MoodId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "MoodId");

                    b.HasIndex("MoodId");

                    b.ToTable("Movie_Moods");
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

            modelBuilder.Entity("MvcIBF.Models.Star", b =>
                {
                    b.Property<int>("StarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StarId"), 1L, 1);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StarId");

                    b.HasIndex("CountryId");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("MvcIBF.Models.VOD", b =>
                {
                    b.Property<int>("VodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VodId"), 1L, 1);

                    b.Property<string>("VodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VodId");

                    b.HasIndex("VodName")
                        .IsUnique();

                    b.ToTable("VODs");
                });

            modelBuilder.Entity("MvcIBF.Models.Material", b =>
                {
                    b.HasOne("MvcIBF.Models.Movie", "Movie")
                        .WithMany("Materials")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Country", b =>
                {
                    b.HasOne("MvcIBF.Models.Country", "Country")
                        .WithMany("Movie_Countries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcIBF.Models.Movie", "Movie")
                        .WithMany("Movie_Countries")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Genre", b =>
                {
                    b.HasOne("MvcIBF.Models.Genre", "Genre")
                        .WithMany("Movie_Genres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcIBF.Models.Movie", "Movie")
                        .WithMany("Movie_Genres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie_Mood", b =>
                {
                    b.HasOne("MvcIBF.Models.Mood", "Mood")
                        .WithMany("Movie_Moods")
                        .HasForeignKey("MoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcIBF.Models.Movie", "Movie")
                        .WithMany("Movie_Moods")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mood");

                    b.Navigation("Movie");
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

            modelBuilder.Entity("MvcIBF.Models.Star", b =>
                {
                    b.HasOne("MvcIBF.Models.Country", "Country")
                        .WithMany("Stars")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MvcIBF.Models.Country", b =>
                {
                    b.Navigation("Movie_Countries");

                    b.Navigation("Stars");
                });

            modelBuilder.Entity("MvcIBF.Models.Genre", b =>
                {
                    b.Navigation("Movie_Genres");
                });

            modelBuilder.Entity("MvcIBF.Models.Mood", b =>
                {
                    b.Navigation("Movie_Moods");
                });

            modelBuilder.Entity("MvcIBF.Models.Movie", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("Movie_Countries");

                    b.Navigation("Movie_Genres");

                    b.Navigation("Movie_Moods");

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
