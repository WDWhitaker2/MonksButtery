using MonksInn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.BeerLibrary
{
    public class AddViewModel
    {
        [Required]
        [Display(Name ="Beer Name")]
        public string BeerName { get; set; }
        [Required]
        [Display(Name = "Brewery Name")]
        public string BreweryName { get; set; }
        
        public decimal? Strength { get; set; }
        public string SubType { get; set; }
        public string Notes { get; set; }
        public BeerType BeerType { get; set; }

        [Display(Name = "This is a Speciality Beer")]
        public bool IsSpecialityBeer { get; set; }

        public List<string> CurrentTypes { get; set; }
        public List<string> CurrentBreweries { get; set; }

        

        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        [UIHint("UploadImage")]
        public Guid? ImageId { get; set; }
    }
}
