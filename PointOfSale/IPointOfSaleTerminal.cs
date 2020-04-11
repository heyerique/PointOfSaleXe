using System;
namespace PointOfSale.Models
{
    public interface IPointOfSaleTerminal
    {
        /**
         *  <summary>Sets the unit price for a product.</summary>
         *  <param name="productCode">string: product code</param>
         *  <param name="unitPrice">decimal: unit price</param>
        **/
        void SetPricing(string productCode, decimal unitPrice);

        /**
         *  <summary>Sets the volume price for a product.</summary>
         *  <param name="productCode">string: product code</param>
         *  <param name="volumePrice">decimal: volume price</param>
         *  <param name="maxVolume">
         *  The number of product that in a volume set.
         *  </param>
        **/
        void SetPricing(string productCode, decimal volumePrice, int maxVolume);

        /**
         *  <summary>
         *  Scans a product in order to calculate the total price.
         *  </summary>
         *  <param name="productCode">string: product code</param>
         **/
        void ScanProduct(string productCode);

        /**
         *  <summary>
         *  Calculates the total price of the scanned products.
         *  </summary>
         *  <returns>decimal: total price</returns>
         **/
        decimal CalculateTotal();

        IBill Bill { get; }
    }
}
