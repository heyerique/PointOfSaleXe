
using System;
using System.Linq;
using PointOfSale.Models;

namespace PointOfSale
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private Bill _bill;

        private IPointOfSale _pointOfSale;

        public PointOfSaleTerminal()
        {

        }

        public PointOfSaleTerminal(IPointOfSale pointOfSale)
        {
            if (pointOfSale != null)
            {
                _pointOfSale = pointOfSale;
            }
        }

        public decimal CalculateTotal()
        {
            var totalPrice = _bill?.TotalPrice ?? 0;
            _bill = null;

            return totalPrice;
        }

        public IBill Bill => _bill;

        public void ScanProduct(string productCode)
        {
            var productItem = GetProductByCode(productCode);

            if (productItem == null)
            {
                return;
            }

            if (_bill == null)
            {
                _bill = new Bill();
            }

            _bill.AddProduct(productItem);
        }

        public void SetPricing(string productCode, decimal unitPrice)
        {
            var productItem = GetProductByCode(productCode);
            productItem?.SetPrice(unitPrice);
        }

        public void SetPricing(string productCode, decimal volumePrice, int maxVolume)
        {
            var productItem = GetProductByCode(productCode);
            productItem?.SetPrice(volumePrice, maxVolume);
        }

        private Product GetProductByCode(string productCode)
        {
            return _pointOfSale?.Products.FirstOrDefault(item => item.Equals(productCode));
        }
    }
}
