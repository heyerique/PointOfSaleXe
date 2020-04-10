using System;
using System.Collections.Generic;
using PointOfSale.Models;

namespace PointOfSale
{
    public class PointOfSale : IPointOfSale
    {
        public List<Product> Products { get; private set; } = new List<Product>();

        public PointOfSale()
        {
        }

        public PointOfSale(List<Product> products)
        {
            if (products == null)
            {
                return;
            }

            Products = products;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                return;
            }

            Products.Add(product);
        }
     
    }
}
