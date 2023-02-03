using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class CartSession : DatabaseObject
    {
        public List<CartItem> Items { get; set; }
    }
}
