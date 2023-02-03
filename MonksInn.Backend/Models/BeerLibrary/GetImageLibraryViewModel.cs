using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.BeerLibrary
{
    public class GetImageLibraryViewModel
    {
        public string BeerName { get; set; }
        public string BreweryName { get; set; }
        public Guid? ImageId { get; set; }
        public int Size { get; set; }
        public Guid BeerId { get; internal set; }
    }
}
