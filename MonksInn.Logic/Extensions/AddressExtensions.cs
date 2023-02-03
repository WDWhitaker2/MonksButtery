using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    public static class AddressExtensions
    {
        public static string ToAddressString(this StoreUserAddress item, string separator = ", ")
        {
            var addresslinelist = new List<string>();
            addresslinelist.Add(item.AddressLine1);
            addresslinelist.Add(item.AddressLine2);
            addresslinelist.Add(item.City);
            addresslinelist.Add(item.PostalCode);
            addresslinelist.Add(item.Country);
            return string.Join(separator, addresslinelist.Where(a => !string.IsNullOrWhiteSpace(a)));
        }
    }
}
