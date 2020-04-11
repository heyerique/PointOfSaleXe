using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public interface IShoppingListItem
    {
        IProduct Product { get; }
        int Count { get; }
        decimal TotalPrice { get; }
    }
}
