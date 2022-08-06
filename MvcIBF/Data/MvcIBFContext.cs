using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;

namespace MvcIBF.Data
{
    public class MvcIBFContext : DbContext
    {
        public MvcIBFContext (DbContextOptions<MvcIBFContext> options)
            : base(options)
        {
        }
        
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<VOD>? VODs { get; set; }
        public DbSet<Movie_VOD>? Movies_VODs { get; set; }
        public DbSet<Mood>? Moods { get; set; }
        public DbSet<Movie_Mood> Movie_Moods { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie_Genre> Movie_Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Movie_Country> Movie_Countries { get; set; }
        public DbSet<Material> Materials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // encja Movie_VOD
            modelBuilder.Entity<Movie_VOD>().HasKey(o => new { o.MovieId, o.VODId });
            modelBuilder.Entity<Movie_VOD>().HasOne(m => m.Movie)
                .WithMany(mv => mv.Movie_VODs)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_VOD>()
               .HasOne(v => v.VOD)
               .WithMany(mv => mv.Movie_VODs)
               .HasForeignKey(vi => vi.VODId);
            // encja Movie_Mood
            modelBuilder.Entity<Movie_Mood>().HasKey(o => new { o.MovieId, o.MoodId });
            modelBuilder.Entity<Movie_Mood>().HasOne(m => m.Movie)
                .WithMany(mm => mm.Movie_Moods)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_Mood>()
               .HasOne(m => m.Mood)
               .WithMany(mm => mm.Movie_Moods)
               .HasForeignKey(mi => mi.MoodId);
            // encja Movie_Genre
            modelBuilder.Entity<Movie_Genre>().HasKey(o => new { o.MovieId, o.GenreId });
            modelBuilder.Entity<Movie_Genre>().HasOne(m => m.Movie)
                .WithMany(mg => mg.Movie_Genres)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_Genre>()
               .HasOne(g => g.Genre)
               .WithMany(mg => mg.Movie_Genres)
               .HasForeignKey(gi => gi.GenreId);
            //encja Movie_Country
            modelBuilder.Entity<Movie_Country>().HasKey(o => new { o.MovieId, o.CountryId });
            modelBuilder.Entity<Movie_Country>().HasOne(m => m.Movie)
                .WithMany(mc => mc.Movie_Countries)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_Country>()
               .HasOne(c => c.Country)
               .WithMany(mc => mc.Movie_Countries)
               .HasForeignKey(ci => ci.CountryId);
        }
    }
}
