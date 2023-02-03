using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    public static class DeliveryTimeAllocationExtensions
    {

        public static string ToDisplayString(this DeliveryDateAllocation deliveryTimeAllocation)
        {
            return $"{deliveryTimeAllocation.DateAllocation.ToString("dddd dd MMM yyyy")} {deliveryTimeAllocation.CommitedStartHour.ToTimeFormat()} - {deliveryTimeAllocation.CommitedEndHour.ToTimeFormat()}";


        }
    }
}
