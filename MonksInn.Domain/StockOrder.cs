using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class StockOrder:DatabaseObject
    {
        public string UnitSize { get; set; }
        public int Units { get; set; }
        public DateTime ETA { get; set; }
        public bool CreateWeeklyOrder { get; set; }
        
        public Guid? BeerId { get; set; }
        public virtual Beer Beer { get; set; }

        //recieving stock

        public DateTime? ReceivedDate { get; set; }
        public string ReceivedInvoiceNumber { get; set; }
        public string ReceivedUnitSize { get; set; }
        public int? ReceivedUnits { get; set; }
        public decimal? ReceivedUnitPrice { get; set; }
        public Guid? ReceivedBeerId { get; set; }
        public virtual Beer ReceivedBeer { get; set; }

    }
}
