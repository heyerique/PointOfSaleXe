using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public interface IBill
    {
        decimal TotalPrice { get; }
        List<IShoppingListItem> ShoppingList { get; }
    }
}
