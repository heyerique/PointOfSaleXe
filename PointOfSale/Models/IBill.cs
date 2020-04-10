using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public interface IBill
    {
        public decimal TotalPrice { get; }
        public List<IShoppingListItem> ShoppingList { get; }
    }
}
