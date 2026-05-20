
namespace ShoppingCartApp
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items;

        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }

        // Ha az item neve már szerepel (kis-nagybetű független), növeli a mennyiségét
        public void AddItem(string name, double unitPrice, int quantity)
        {
            if (name is not null && name != "" && unitPrice > 0 && quantity > 0)
            {
                CartItem c = new (name, unitPrice, quantity);
                if (!_items.Select(x => x.Name.ToLower()).Contains(name.ToLower())) _items.Add(c);
                else _items.Find(x => x.Name == name).UpdateQuantity(quantity);
            }
            else { throw new ArgumentException(); }
        }

        // true ha megtalálta és törölte, false ha nem szerepelt
        public bool RemoveItem(string name)
        {
            if (_items.Select(x => x.Name.ToLower()).Contains(name.ToLower()))
            {
                _items.Remove(_items.Find(x => x.Name.ToLower() == name.ToLower()));
                return true;
            }
            else return false;
        }

        public int GetItemCount()
        {
            return _items.Count;
        }

        // Összeg = minden item (UnitPrice * Quantity) összege
        public decimal GetTotal()
        {
            double sum = 0;
            if (_items.Count > 0)
            {
                foreach (CartItem c in _items)
                {
                    sum += c.UnitPrice * c.Quantity;
                }
                return (decimal)sum;
            }
            else return 0;
        }

        public IReadOnlyList<CartItem> GetItems()
        {
            return _items;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
