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

        /**
         * <summary>
         * Initialises a ShoppingListItem object with a product object.
         * </summary>
         * <param name="product"><see cref="IProduct"/>: product object</param>
         **/
        public ShoppingListItem(IProduct product)
        {
            SetProduct(product);
        }

        /**
         * <summary>Sets the value of Product.</summary>
         * <param name="product"><see cref="IProduct"/>: product object</param>
         * <exception cref="NullProductException"></exception>
         * <exception cref="NullPriceException"></exception>
         **/
        public void SetProduct(IProduct product)
        {
            if (product == null)
            {
                throw new NullProductException();
            }

            if (!product.HasPrice)
            {
                throw new NullPriceException($"The price for the product ${product.Code} is not set.");
            }

            Product = product;
        }

        /**
         * <summary>Adds the count of the product.</summary>
         * <param name="count">
         * int: the count to be added. Default value is 1.
         * </param>
         * <exception cref="ArgumentOutOfRangeException"></exception>
         **/
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
                    || Count == 0
                    || !Product.HasPrice)
                {
                    return 0;
                }

                if (!Product.Price.HasVolumePrice
                    || Product.Price.MaxVolume == 1)
                {
                    return (decimal)Product.Price.UnitPrice * Count;
                }

                var unitCount = Count % (decimal)Product.Price.MaxVolume;
                var unitTotalPrice = unitCount * (decimal)Product.Price.UnitPrice;

                var volumeCount = Math.Floor((decimal)(Count / (int)Product.Price.MaxVolume));
                var volumeTotalPrice = volumeCount * (decimal)Product.Price.VolumePrice;

                return volumeTotalPrice + unitTotalPrice;
            }
        }
    }
}
