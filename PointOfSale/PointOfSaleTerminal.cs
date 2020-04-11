
using System;
using System.Linq;
using PointOfSale.Models;

namespace PointOfSale
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private Bill _bill;

        private readonly IPointOfSale _pointOfSale;

        public PointOfSaleTerminal()
        {

        }

        /**
         *  <summary>
         *  Initialises the instance of PointOfSaleTerminal
         *  with a instance of IPointOfSale.
         *  </summary>
         *  <param name="pointOfSale"><see cref="IPointOfSale"/>
         *  : instance of IPointOfSale
         *  </param>
         *  <exception cref="ArgumentNullException"></exception>
         **/
        public PointOfSaleTerminal(IPointOfSale pointOfSale)
        {
            _pointOfSale = pointOfSale
                ?? throw new ArgumentNullException("PointOfSale cannot be empty.");
        }

        /**
         *  <summary>
         *  Calculates the total price of the scanned products.
         *  </summary>
         *  <returns>decimal: total price</returns>
         **/
        public decimal CalculateTotal()
        {
            var totalPrice = _bill?.TotalPrice ?? 0;
            _bill = null;

            return totalPrice;
        }

        public IBill Bill => _bill;

        /**
         *  <summary>
         *  Scans a product in order to calculate the total price.
         *  </summary>
         *  <param name="productCode">string: product code</param>
         **/
        public void ScanProduct(string productCode)
        {
            var productItem = GetProductByCode(productCode);

            if (_bill == null)
            {
                _bill = new Bill();
            }

            _bill.AddProduct(productItem);
        }

        /**
         *  <summary>Sets the unit price for a product.</summary>
         *  <param name="productCode">string: product code</param>
         *  <param name="unitPrice">decimal: unit price</param>
        **/
        public void SetPricing(string productCode, decimal unitPrice)
        {
            var productItem = GetProductByCode(productCode);
            productItem.SetPrice(unitPrice);
        }

        /**
         *  <summary>Sets the volume price for a product.</summary>
         *  <param name="productCode">string: product code</param>
         *  <param name="volumePrice">decimal: volume price</param>
         *  <param name="maxVolume">
         *  The number of product that in a volume set.
         *  </param>
        **/
        public void SetPricing(string productCode, decimal volumePrice, int maxVolume)
        {
            var productItem = GetProductByCode(productCode);
            productItem.SetPrice(volumePrice, maxVolume);
        }

        /**
         *  <summary>
         *  Gets the product object from PointOfSale by querying with product code
         *  </summary>
         *  <param name="productCode">string: product code</param>
         *  <returns><see cref="Product"/>: product object</returns>
         *  <exception cref="NullReferenceException">
         *  PointOfSale is not set or the product doesn't exist.
         *  </exception>
         **/
        private Product GetProductByCode(string productCode)
        {
            var item = _pointOfSale?.Products.FirstOrDefault(item => item.Equals(productCode));

            if (item == null)
            {
                throw new NullReferenceException($"No such product: {productCode}\n");
            }

            return item;
        }
    }
}
