using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.BeerLibrary
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            BeerList = new List<Beer>();
        }
        public List<Beer> BeerList { get; set; }
    }
}
