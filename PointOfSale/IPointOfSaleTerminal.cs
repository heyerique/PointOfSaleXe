using System;
namespace PointOfSale.Models
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(string productCode, decimal unitPrice);
        void SetPricing(string productCode, decimal volumePrice, int maxVolume);
        void ScanProduct(string productCode);
        decimal CalculateTotal();
        IBill Bill { get; }
    }
}
