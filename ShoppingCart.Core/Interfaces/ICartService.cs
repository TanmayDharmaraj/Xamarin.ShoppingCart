using ShoppingCart.Core.Models;
using System.Collections.Generic;

namespace ShoppingCart.Core.Interfaces
{
    public interface ICartService
    {
        List<Cart> GetAllItems();

        void Add(Cart item);

        Cart GetItem(string SKU);

        void RemoveItem(Cart item);
    }
}