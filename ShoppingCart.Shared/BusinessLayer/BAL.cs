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
using ShoppingCart.Shared.Models;

namespace ShoppingCart.Shared.BusinessLayer
{
    public class BAL
    {
        private static List<Cart> _CartItems;

        public static List<Cart> CartItems
        {
            get { return _CartItems; }
            set { _CartItems = value; }
        }



    }
}