using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Models
{
    public class Bill : IBill
    {
        private readonly List<ShoppingListItem> _shoppingListItems = new List<ShoppingListItem>();

        public List<IShoppingListItem> ShoppingList => _shoppingListItems.Select(item => item as IShoppingListItem).ToList();

        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                return;
            }

            var listItem = _shoppingListItems.FirstOrDefault(item => item.Product.Equals(product));

            if (listItem == null)
            {
                listItem = new ShoppingListItem(product);
                _shoppingListItems.Add(listItem);
            }

            listItem.AddCount();
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in ShoppingList)
                {
                    totalPrice += item.TotalPrice;
                }

                return totalPrice;
            }
        }
    }
}
