using ShoppingCartApp;

namespace ShoppingCartAppTests
{
    [TestClass]
    public class CartItemTests
    {
        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var item = new CartItem("Apple", 1.50, 3);
            Assert.AreEqual("Apple", item.Name);
            Assert.AreEqual(1.50, item.UnitPrice);
            Assert.AreEqual(3, item.Quantity);
        }

        [TestMethod]
        public void Constructor_Checks()
        {
            var item = new CartItem("Apple", 1.50, 3);
            Assert.IsNotNull(item.Name);
            Assert.IsTrue(item.UnitPrice > 0);
            Assert.IsTrue(item.Quantity > 0);
        }
        // TODO: null/üres name -> ArgumentException
        // TODO: unitPrice <= 0 -> ArgumentException
        // TODO: quantity <= 0 -> ArgumentException

        [TestMethod]
        public void GetTotal_MultipleQuantity()
        {
            var item = new CartItem("Banana", 0.75, 4);
            Assert.AreEqual(3.00, item.GetLineTotal());
        }

        [TestMethod]
        public void UpdateQuantity_ValidValue()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(5);
            Assert.AreEqual(5, item.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Quantity_Check()
        {
            var item = new CartItem("Banana", 0.75, 4);
            item.UpdateQuantity(-4);
            item.UpdateQuantity(0);
        }
        // TODO: quantity <= 0 -> ArgumentException
    }

    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCart CreateCartWithItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);
            cart.AddItem("Bread", 2.50, 1);
            return cart;
        }

        [TestMethod]
        public void AddItem_NewItem()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_QuantityIncrease()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            cart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(1, cart.GetItemCount());
        }
        // TODO: ugyanolyan nevű item hozzáadása, mennyiséget növel annál az adott item-nél (nincs új item)
        // TODO: érvénytelen argumentumok -> ArgumentException

        [TestMethod]
        public void RemoveItem_ExistingItem()
        {
            var cart = CreateCartWithItems();
            bool result = cart.RemoveItem("Apple");
            Assert.IsTrue(result);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void RemoveItem_Checks()
        {
            var cart = new ShoppingCart();
            Assert.IsFalse(cart.RemoveItem("Test"));

            cart.AddItem("Apple", 1.00, 2);
            Assert.IsTrue(cart.RemoveItem("apple"));
        }
        // TODO: nem létező item -> false
        // TODO: törlés kis-nagybetű független-e ("apple" törli az "Apple"-t)

        [TestMethod]
        public void GetTotal_MultipleItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);  // 3.00
            cart.AddItem("Bread", 2.50, 2);  // 5.00
            Assert.AreEqual(8.00m, cart.GetTotal());
        }

        [TestMethod]
        public void Cart_Checks()
        {
            var cart = new ShoppingCart();
            Assert.AreEqual(0, cart.GetItemCount());

            cart.AddItem("Apple", 1.00, 2);
            cart.RemoveItem("apple");
            Assert.AreEqual(0, cart.GetItemCount());
        }
        // TODO: üres kosár -> 0
        // TODO: item törlése után helyes-e az összeg

        [TestMethod]
        public void Clear_CartWithItems()
        {
            var cart = CreateCartWithItems();
            cart.Clear();
            Assert.AreEqual(0, cart.GetItemCount());
            Assert.AreEqual(0m, cart.GetTotal());
        }

        [TestMethod]
        public void Clear_Test()
        {
            var cart = new ShoppingCart();
            cart.Clear();
        }
        // TODO: üres kosáron Clear() nem dob kivételt
    }

    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void ApplyPercentage_TenPercent()
        {
            var discount = new Discount();
            Assert.AreEqual(180, discount.ApplyPercentage(200, 10));
        }

        [TestMethod]
        public void Discount_Tests()
        {
            var discount = new Discount();
            Assert.AreEqual(100, discount.ApplyPercentage(100, 0));
            Assert.AreEqual(0, discount.ApplyPercentage(100, 100));
            discount.ApplyPercentage(100, 200);
        }
        // TODO: 0% -> változatlan összeg
        // TODO: 100% -> 0
        // TODO: percent > 100 -> ArgumentException

        [TestMethod]
        public void ApplyFixed_AmountLessThanTotal()
        {
            var discount = new Discount();
            Assert.AreEqual(75, discount.ApplyFixed(100, 25));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Discount_Negative()
        {
            var discount = new Discount();
            discount.ApplyPercentage(100, -5);
        }
        // TODO: negatív discountAmount -> ArgumentException

        [TestMethod]
        public void IsValid_PositiveValue()
        {
            var discount = new Discount();
            Assert.IsTrue(discount.IsValid(15));
            Assert.IsFalse(discount.IsValid(0));
            Assert.IsFalse(discount.IsValid(-5));
        }
        // TODO: 0 -> false
        // TODO: negatív -> false
    }
}
