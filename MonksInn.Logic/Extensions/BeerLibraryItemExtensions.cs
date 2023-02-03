using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Logic.Extensions
{
    public static class BeerLibraryItemExtensions
    {
        public static List<string> ValidationErrors(this Beer item)
        {
            var list = new List<string>();

            if (!item.ImageId.HasValue)
                list.Add("This beer has no pump clip image.");

            if (string.IsNullOrWhiteSpace(item.Notes))
                list.Add("The beer has no tasting notes.");
            
            if (string.IsNullOrWhiteSpace(item.SubType))
                list.Add("The beer has no sub Type set.");

            if (!item.Strength.HasValue)
                list.Add("The beer has no ABV strength set.");

            return list;
        }
    }
}
