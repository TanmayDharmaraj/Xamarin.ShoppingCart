using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Core.Services
{
    public class CartService : ICartService
    {
        private List<Cart> Items = new List<Cart>();

        public void Add(Cart item)
        {
            Items.Add(item);
        }

        public List<Cart> GetAllItems()
        {
            return Items;
        }

        public Cart GetItem(string SKU)
        {
            return (from i in Items
                    where i.SKU.Equals(SKU)
                    select i).FirstOrDefault();
        }

        public void RemoveItem(Cart item)
        {
            Items.Remove(item);
        }
    }
}