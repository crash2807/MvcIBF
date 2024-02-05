using MvcIBF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace MvcIBF.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var context = new MvcIBFContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcIBFContext>>()))
            {
                
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB has been seeded
                }
                var movie1 = new Movie
                {
                    MovieTitle = "Spider-Man: Bez drogi do domu",
                    MovieDescription = "Kiedy cały świat dowiaduje się, że pod maską Spider Mana skrywa się Peter Parker, superbohater decyduje się zwrócić o pomoc do Doktora Strange'a.",
                    ReleaseDate = new DateTime(2021, 12, 17),
                    Runtime = 150
                };
                var movie2 = new Movie
                {
                    MovieDescription = "Historia Jordana Belforta, brokera, którego błyskawiczna droga na szczyt i rozrzutny styl życia wzbudziły zainteresowanie FBI.",
                    MovieTitle = "Wilk z Wall Street",
                    Runtime = 179,
                    ReleaseDate = new DateTime(2014, 1, 3)
                };

                
                context.AddRange(
                    new Movie
                    {
                        MovieTitle = "Botoks",
                        MovieDescription = "Losy czterech kobiet pracujących w służbie medycznej splatają się w szpitalu, gdzie dochodzi do wielu nielegalnych przedsięwzięć.",
                        ReleaseDate = new DateTime(2017, 9, 29),
                        Runtime = 135

                    }, new Movie
                    {
                        MovieTitle = "Last Minute",
                        MovieDescription = "Matka Tomka, wygrywając wycieczkę do Egiptu, zabiera całą rodzinę ze sobą. Na miejscu dochodzi do wielu niefortunnych zdarzeń.",
                        ReleaseDate = new DateTime(2013, 2, 22),
                        Runtime = 82
                    }, new Movie { 
                        MovieTitle = "Top Gun: Maverick",
                        MovieDescription= "Po ponad 20 latach służby w lotnictwie marynarki wojennej, Pete \"Maverick\" Mitchell zostaje wezwany do legendarnej szkoły Top Gun. Ma wyszkolić nowe pokolenie pilotów do niezwykle trudnej misji.",
                        ReleaseDate= new DateTime(2022,5,27),
                        Runtime=131
                    }, new Movie
                    {
                        MovieTitle= "Piła",
                        MovieDescription= "Dwóch mężczyzn budzi się przykutych do zardzewiałej rury w piwnicy. Zdają sobie sprawę, że są uczestnikami krwawej gry szaleńca.",
                        ReleaseDate=new DateTime(2005,2,25),
                        Runtime=103
                    }, new Movie
                    {
                        MovieTitle= "Podziemny krąg",
                        MovieDescription= "Cierpiący na bezsenność mężczyzna poznaje gardzącego konsumpcyjnym stylem życia Tylera Durdena, który jest jego zupełnym przeciwieństwem.",
                        ReleaseDate=new DateTime(2000,2,11),
                        Runtime=139
                    }, movie1,movie2
                    ) ;
                context.AddRange(new VOD { VodName = "Netflix"//,Movies= (ICollection<Movie>)movie2
                }, new VOD { VodName= "HBO Max"}, new VOD { VodName = "Disney+"//, Movies = (ICollection<Movie>)movie1
                });
                context.SaveChanges();
            }
        }
    }
}
