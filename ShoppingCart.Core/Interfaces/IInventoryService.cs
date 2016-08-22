using ShoppingCart.Core.Models;
using System.Collections.Generic;

namespace ShoppingCart.Core.Interfaces
{
    public interface IInventoryService
    {
        List<Inventory> GetInventory();
    }
}