using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Models
{
    public class Bill : IBill
    {
        private readonly List<ShoppingListItem> _shoppingListItems = new List<ShoppingListItem>();

        public List<IShoppingListItem> ShoppingList => _shoppingListItems.Select(item => item as IShoppingListItem).ToList();

        /**
         * <summary>Adds a product into the shopping list.</summary>
         * <param name="product"><see cref="IProduct"/>: product object</param>
         * <exception cref="ArgumentNullException"></exception>
        **/
        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.");
            }

            var listItem = _shoppingListItems.FirstOrDefault(item => item.Product.Equals(product));

            if (listItem == null)
            {
                listItem = new ShoppingListItem(product);
                _shoppingListItems.Add(listItem);
            }

            listItem.AddCount();
        }

        public decimal TotalPrice => _shoppingListItems.Select(item => item.TotalPrice).Sum();
    }
}
