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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable(nameof(Movie))
                .HasMany(c => c.VODs)
                .WithMany(i => i.Movies);

        }
    }
}
