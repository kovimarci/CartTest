using System.Xml.Linq;

namespace ShoppingCartApp
{
    public class CartItem
    {
        public string Name { get; }
        public double UnitPrice { get; }
        public int Quantity { get; private set; }

        // name nem lehet null/üres, unitPrice > 0, quantity >= 1
        public CartItem(string name, double unitPrice, int quantity)
        {
            if (name is not null && name != "" && unitPrice > 0 && quantity >= 1 )
            {
                Name = name;
                UnitPrice = unitPrice;
                Quantity = quantity;
            } 
            else { throw new ArgumentException(); }
        }

        // UnitPrice * Quantity
        public double GetLineTotal()
        {
            return UnitPrice * Quantity;
        }
        
        // quantity >= 1, különben ArgumentException
        public void UpdateQuantity(int quantity)
        {
            if (quantity >= 1)
            {
                Quantity = quantity;
            }
            else { throw new ArgumentException(); }
        }
    }
}
