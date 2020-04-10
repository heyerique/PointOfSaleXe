using System;
using System.Collections.Generic;

namespace PointOfSale.Models
{
    public interface IPointOfSale
    {
        List<Product> Products { get; }
    }
}
