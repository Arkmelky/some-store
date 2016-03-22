using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();

        public void AddItem(StoreProduct storeProduct, int quantity)
        {
            CartItem cartItem = cartItems
                .Where(o => o.StoreProduct.StoreProductId == storeProduct.StoreProductId)
                .FirstOrDefault();

            if (cartItem == null)
            {
                cartItems.Add(new CartItem
                {
                    StoreProduct = storeProduct,
                    Quantity = quantity
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveItem(StoreProduct storeProduct)
        {
            cartItems.RemoveAll(o => o.StoreProduct.StoreProductId == storeProduct.StoreProductId);
        }

        public decimal CalcTotalPrice()
        {
            return cartItems.Sum(o => o.Quantity * o.StoreProduct.Price);
        }

        public void Clear()
        {
            cartItems.Clear();
        }

        public IEnumerable<CartItem> Items
        {
            get { return cartItems; }
        }
    }



    public class CartItem
    {
        public StoreProduct StoreProduct { get; set; }
        public int Quantity { get; set; }
    }
}
