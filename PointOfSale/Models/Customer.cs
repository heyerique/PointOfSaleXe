using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public class Customer
    {
        public List<IProduct> ShoppingCart { get; set; } = new List<IProduct>();
        public IBill Bill { get; set; }
    }
}
