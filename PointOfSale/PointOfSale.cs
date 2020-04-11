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
            Products = products
                ?? throw new ArgumentNullException("Products cannot be null.");
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.");
            }

            Products.Add(product);
        }
     
    }
}
