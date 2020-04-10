
namespace PointOfSale.Models
{
    public class Price : IPrice
    {
        public decimal UnitPrice { get; private set; }

        public decimal VolumePrice { get; private set; }

        public int MaxVolume { get; private set; } = 1;

        public Price()
        {
        }

        public Price(decimal unitPrice)
        {
            SetPrice(unitPrice);
        }

        public Price(decimal unitPrice, decimal volumePrice, int maxVolume)
        {
            SetPrice(unitPrice);
            SetPrice(volumePrice, maxVolume);
        }

        public void SetPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                return;
            }

            UnitPrice = unitPrice;
        }

        public void SetPrice(decimal volumePrice, int maxVolume)
        {
            if (volumePrice <= 0 || maxVolume < 1)
            {
                return;
            }

            VolumePrice = volumePrice;
            MaxVolume = maxVolume;
        }
    }
}
