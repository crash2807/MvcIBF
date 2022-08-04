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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie_VOD>().HasKey(o => new { o.MovieId, o.VODId });
            modelBuilder.Entity<Movie_VOD>().HasOne(m => m.Movie)
                .WithMany(mv => mv.Movie_VODs)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_VOD>()
               .HasOne(v => v.VOD)
               .WithMany(mv => mv.Movie_VODs)
               .HasForeignKey(vi => vi.VODId);
            modelBuilder.Entity<Movie_Mood>().HasKey(o => new { o.MovieId, o.MoodId });
            modelBuilder.Entity<Movie_Mood>().HasOne(m => m.Movie)
                .WithMany(mm => mm.Movie_Moods)
                .HasForeignKey(mi => mi.MovieId);
            modelBuilder.Entity<Movie_Mood>()
               .HasOne(m => m.Mood)
               .WithMany(mm => mm.Movie_Moods)
               .HasForeignKey(mi => mi.MoodId);

        }
    }
}
