using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Models.StockOrderLogic
{
    public class ReceiveStockLogicModel
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal UnitPrice { get;  set; }
        public int Units { get;  set; }
        public string UnitSize { get;  set; }
        public Guid BeerId { get;  set; }
        public DateTime SellByDate { get;  set; }
        public decimal BeerStrength { get; set; }
    }
}
