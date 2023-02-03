using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    public static class StockOrderExtensions
    {
        public static List<string> ValidationErrors(this StockOrder order)
        {
            var list = new List<string>();

            if (order.ReceivedBeerId.HasValue && order.BeerId != order.ReceivedBeerId)
                list.Add("The received beer does not match what was ordered.");

            if (order.ReceivedDate.HasValue && order.ReceivedDate.Value.Date > order.ETA.Date)
                list.Add("The beer was received after the agreed ETA.");

            if (!string.IsNullOrWhiteSpace(order.ReceivedUnitSize) && order.ReceivedUnitSize != order.UnitSize)
                list.Add("The received beer has a different unit size to what was ordered.");

            if (order.ReceivedUnits.HasValue && order.ReceivedUnits != order.Units)
                list.Add("The received units of beer is different to what was ordered.");


            return list;
        }
    }
}
