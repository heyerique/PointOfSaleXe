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
         **/
        public PointOfSale(List<Product> products)
        {
            if (products != null)
            {
                Products = products;
            }
        }

        /**
         *  <summary>Adds a product to the PointOfSale.</summary>
         *  <param name="product"><see cref="Product"/>: product object</param>
         *  <exception cref="NullProductException"></exception>
         *  <exception cref="DuplicatedProductException"></exception>
         **/
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new NullProductException();
            }

            if (Products.Any(item => item.Equals(product)))
            {
                throw new DuplicatedProductException($"Product {product.Code} is already existed.");
            }

            Products.Add(product);
        }
     
    }
}
