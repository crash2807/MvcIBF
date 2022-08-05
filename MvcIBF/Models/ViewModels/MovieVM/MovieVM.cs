﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.MovieVM
{
    public class MovieVM
    {
        public int MovieId { get; set; }
        [Display(Name = "Tytuł")]
        [Required]

        public string? MovieTitle { get; set; }
        [Display(Name = "Data premiery")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Czas trwania (min)")]
        public int Runtime { get; set; }
        [Display(Name = "Opis")]
        [Required]
        public string? MovieDescription { get; set; }

        [Display(Name = "VOD")]
        public IEnumerable<SelectListItem>? VODsList { get; set; }
        
        public int[]? SelectedVODs { get; set; }
        [Display(Name = "VOD")]

        public List<string>? VodNames { get; set; }
        [Display(Name = "Nastroje")]
        public IEnumerable<SelectListItem>? MoodsList { get; set; }

        public int[]? SelectedMoods { get; set; }
        [Display(Name = "Nastroje")]

        public List<string>? MoodNames { get; set; }

        [Display(Name = "Gatunki")] 
        public IEnumerable<SelectListItem>? GenresList { get; set; }

        public int[]? SelectedGenres { get; set; }
        [Display(Name = "Gatunki")]

        public List<string>? GenreNames { get; set; }
        [Display(Name = "Kraj produkcji")]
        public IEnumerable<SelectListItem>? CountriesList { get; set; }

        public int[]? SelectedCountries { get; set; }
        [Display(Name = "Kraj produkcji")]

        public List<string>? CountryNames { get; set; }
    }
}
