
using System;

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
                throw new ArgumentOutOfRangeException("Price should be larger than 0.");
            }

            UnitPrice = unitPrice;
        }

        public void SetPrice(decimal volumePrice, int maxVolume)
        {
            if (volumePrice <= 0 || maxVolume < 1)
            {
                throw new ArgumentOutOfRangeException("Price should be larger than 0 and volume should be larger than 0.");
            }

            if (maxVolume == 1)
            {
                SetPrice(volumePrice);
                return;
            }

            VolumePrice = volumePrice;
            MaxVolume = maxVolume;
        }
    }
}
