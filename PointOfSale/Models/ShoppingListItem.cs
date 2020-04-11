using System;
namespace PointOfSale.Models
{
    public class ShoppingListItem : IShoppingListItem
    {
        public IProduct Product { get; private set; }

        public int Count { get; private set; }

        public ShoppingListItem()
        {
        }

        public ShoppingListItem(IProduct product)
        {
            SetProduct(product);
        }

        public void SetProduct(IProduct product)
        {
            Product = product
                ?? throw new ArgumentNullException("Product cannot be null.");
        }

        public void AddCount(int count = 1)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("Count should be larger than 0.");
            }

            Count += count;
        }

        public decimal TotalPrice
        {
            get
            {
                if (Product == null
                    || Product.Price == null
                    || Count == 0)
                {
                    return 0;
                }

                if (Product.Price.MaxVolume == 1)
                {
                    return Product.Price.UnitPrice * Count;
                }

                var unitCount = Count % Product.Price.MaxVolume;
                var unitTotalPrice = unitCount * Product.Price.UnitPrice;

                var volumeCount = Math.Floor((decimal)(Count / Product.Price.MaxVolume));
                var volumeTotalPrice = volumeCount * Product.Price.VolumePrice;

                return volumeTotalPrice + unitTotalPrice;
            }
        }
    }
}
