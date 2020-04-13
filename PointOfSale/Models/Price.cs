
using System;

namespace PointOfSale.Models
{
    public class Price : IPrice
    {
        public decimal? UnitPrice { get; private set; }

        public decimal? VolumePrice { get; private set; }

        public int? MaxVolume { get; private set; }

        public bool HasVolumePrice => VolumePrice != null && MaxVolume != null;

        public Price()
        {
        }

        /**
         * <summary>Sets the unit price for a product.</summary>
         * <param name="unitPrice">decimal: unit price</param>
         * <exception cref="ArgumentOutOfRangeException"></exception>
        **/
        public void SetPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException("Price should be larger than 0.");
            }

            UnitPrice = unitPrice;
        }

        /**
         * <summary>Sets the volume price for a product.</summary>
         * <param name="volumePrice">decimal: volume price</param>
         * <param name="maxVolume">
         * int: the number of product that in a volume set.
         * </param>
         * <exception cref="ArgumentOutOfRangeException"></exception>
        **/
        public void SetPrice(decimal volumePrice, int maxVolume)
        {
            if (volumePrice < 0 || maxVolume < 1)
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
