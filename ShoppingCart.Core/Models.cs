namespace ShoppingCart.Core.Models
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

    public class Cart
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public double CostPerUnit { get; set; }
    }
}