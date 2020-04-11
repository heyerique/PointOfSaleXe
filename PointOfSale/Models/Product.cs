using System;
namespace PointOfSale.Models
{
    public abstract class Product : IProduct
    {
        private readonly Price _price = new Price();

        public string Code { get; private set; }

        public IPrice Price => _price;

        public Product()
        {
        }

        /**
         * <summary>Initialises a Product object with product code.</summary>
         * <param name="code">string: product code</param>
         **/
        public Product(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException("Product code cannot be empty");
            }

            Code = code;
        }

        /**
         * <summary>
         * Initialises a Product object with product code and unit price.
         * </summary>
         * <param name="code">string: product code</param>
         * <param name="unitPrice">decimal: unit price</param>
         **/
        public Product(string code, decimal unitPrice) : this(code)
        {
            SetPrice(unitPrice);
        }

        /**
         * <summary>Sets the unit price for the product.</summary>
         * <param name="unitPrice">decimal: unit price</param>
        **/
        public void SetPrice(decimal unitPrice)
        {
            _price.SetPrice(unitPrice);
        }

        /**
         * <summary>Sets the volume price for the product.</summary>
         * <param name="volumePrice">decimal: volume price</param>
         * <param name="maxVolume">
         * int: the number of product that in a volume set.
         * </param>
        **/
        public void SetPrice(decimal volumePrice, int maxVolume)
        {
            _price.SetPrice(volumePrice, maxVolume);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        /**
         * <summary>
         * Compares two products to check if they are the same.
         * </summary>
         * <param name="code">string: product code</param>
         * <returns>boolean: true is matched, false is not match.</returns>
         **/
        public override bool Equals(object obj)
        {
            if (!(obj is IProduct product))
            {
                return false;
            }

            return Code.ToLower() == product.Code.ToLower();
        }

        /**
         * <summary>
         * Compares two products with their product code
         * to check if they are the same.
         * </summary>
         * <param name="code">string: product code</param>
         * <returns>boolean: true is matched, false is not matched.</returns>
         **/
        public bool Equals(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            return Code.ToLower() == code.ToLower();
        }
    }
}
