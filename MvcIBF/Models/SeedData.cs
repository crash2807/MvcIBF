using MvcIBF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }
                
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
                    }
                    ) ;
                context.SaveChanges();
            }
        }
    }
}
