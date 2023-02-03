using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Logic.Extensions
{
    public static class PubLocationExtensions
    {
        public static string GetLocationStatus(this PubLocation location)
        {
            var result = "None";

            if(location.DefaultIsDeliveryLocation && location.DefaultIsTakeawayLocation)
            {
                result = "Both";
            }
            else if (location.DefaultIsDeliveryLocation)
            {
                result = "Delivery";
            }
            else if(location.DefaultIsTakeawayLocation)
            {
                result = "Takaway";
            }


            return result;
        }
    }
}
