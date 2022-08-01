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

        }
    }
}
