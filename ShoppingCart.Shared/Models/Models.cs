using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Shared.Models
{
    public class Cart
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
    }
}
