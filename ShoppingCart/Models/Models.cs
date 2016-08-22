using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShoppingCart.Models
{
    public class Inventory
    {
        public string SKU { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
    }

    //public class Cart
    //{
    //    public string SKU { get; set; }
    //    public int Quantity { get; set; }
    //}
}