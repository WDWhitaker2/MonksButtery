using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Tap
{
    public class TapViewModel
    {
        public List<TappedStockItem> StockItems { get; internal set; }
    }
}
