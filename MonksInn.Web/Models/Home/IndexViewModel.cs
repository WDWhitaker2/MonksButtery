using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Home
{
    public class IndexViewModel
    {
        public List<TappedStockItem> TappedStock { get; set; }
        public string CurrentPub { get; internal set; }
        public List<string> OtherPubLocations { get; internal set; }
    }
}
