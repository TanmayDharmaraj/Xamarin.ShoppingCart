using ShoppingCart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Services
{
    public interface ICartService
    {
        List<Cart> GetAllItems();
        void Add(Cart item);
        Cart GetItem(string SKU);
        void RemoveItem(Cart item);
    }
}
