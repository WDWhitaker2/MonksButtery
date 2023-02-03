using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Models.Cellar
{
    public class CellarViewModel
    {
        public List<CellarStockItem> StockItems { get; internal set; }
    }
}
