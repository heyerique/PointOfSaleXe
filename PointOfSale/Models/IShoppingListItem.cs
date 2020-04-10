using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public interface IShoppingListItem
    {
        public IProduct Product { get; }
        public int Count { get; }
        public decimal TotalPrice { get; }
    }
}
