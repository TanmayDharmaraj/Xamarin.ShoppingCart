using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Models
{
    public class Cart
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double CostPerUnit { get; set; }
    }
}
