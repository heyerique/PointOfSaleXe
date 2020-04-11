using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Models;

namespace PointOfSale
{
    public class PointOfSale : IPointOfSale
    {
        public List<Product> Products { get; private set; } = new List<Product>();

        public PointOfSale()
        {
        }

        /**
         * <summary>
         * Initialises a PointOfSale instance with a product list.
         * </summary>
         * <param name="products">
         * List(<see cref="Product"/>): a product list
         * </param>
         * <exception cref="ArgumentNullException"></exception>
         **/
        public PointOfSale(List<Product> products)
        {
            Products = products
                ?? throw new ArgumentNullException("Products cannot be null.");
        }

        /**
         *  <summary>Adds a product to the PointOfSale.</summary>
         *  <param name="product"><see cref="Product"/>: product object</param>
         *  <exception cref="ArgumentNullException"></exception>
         **/
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.");
            }

            if (Products.Any(item => item.Equals(product)))
            {
                throw new InvalidOperationException($"Product is already existed: {product.Code}");
            }

            Products.Add(product);
        }
     
    }
}
