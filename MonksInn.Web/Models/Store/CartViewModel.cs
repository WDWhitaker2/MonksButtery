using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Store
{
    public class CartViewModel
    {
        public Guid? CartId { get; internal set; }
        public CartSession Cart { get; internal set; }
    }
}
