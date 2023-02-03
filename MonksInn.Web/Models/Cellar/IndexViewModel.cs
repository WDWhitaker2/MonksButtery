using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Cellar
{
    public class IndexViewModel
    {
        public List<CellarStockItem> CellarStock { get; internal set; }
    }
}
