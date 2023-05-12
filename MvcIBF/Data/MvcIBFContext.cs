using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;

namespace MvcIBF.Data
{
    public class MvcIBFContext : IdentityDbContext
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
        public DbSet<Function> Functions { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Movie_Star_Function> Movie_Stars_Functions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
            //encja Movie_Star_Function
            modelBuilder.Entity<Movie_Star_Function>().HasKey(o => new { o.MovieId, o.StarId, o.FunctionId });
            modelBuilder.Entity<Movie_Star_Function>().HasOne(m => m.Movie)
                .WithMany(m => m.Movie_Stars_Functions)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_Star_Function>().HasOne(s => s.Star)
                .WithMany(m => m.Movie_Stars_Functions)
                .HasForeignKey(si => si.StarId);
            modelBuilder.Entity<Movie_Star_Function>().HasOne(f => f.Function)
                .WithMany(m => m.Movie_Stars_Functions)
                .HasForeignKey(fi => fi.FunctionId);
            //encja Rating
            modelBuilder.Entity<Rating>().HasKey(o => new { o.MovieId, o.ApplicationUserId });
            modelBuilder.Entity<Rating>().HasOne(m => m.Movie)
                .WithMany(r => r.Ratings)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Rating>().HasOne(u=> u.ApplicationUser)
                .WithMany(u=> u.Ratings)
                .HasForeignKey(ui=> ui.ApplicationUserId);
            //encja Friendship
            modelBuilder.Entity<Friendship>().HasKey(o => new { o.FriendshipId });
            modelBuilder.Entity<Friendship>().HasOne(u => u.ApplicationUser1)
                .WithMany()
                .HasForeignKey(u => u.User1Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friendship>().HasOne(u => u.ApplicationUser2)
                .WithMany()
                .HasForeignKey(u=> u.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
