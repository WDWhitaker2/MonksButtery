using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Models.StockOrderLogic
{
    public class AddLogicModel
    {
        public string UnitSize { get; set; }
        public int Units { get; set; }
        public DateTime ETA { get; set; }
        public bool CreateWeeklyOrder { get; set; }
        public Guid? BeerId { get; set; }
    }
}
