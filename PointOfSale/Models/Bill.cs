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
         * <exception cref="ArgumentNullException">Product is null</exception>
         * <exception cref="NullReferenceException">Price is null</exception>
        **/
        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                throw new NullProductException();
            }

            if (!product.HasPrice)
            {
                throw new NullPriceException($"The price for the product ${product.Code} is not set.");
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
