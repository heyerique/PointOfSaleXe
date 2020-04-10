
namespace PointOfSale.Models
{
    public class Product : IProduct
    {
        private readonly Price _price = new Price();

        public string Code { get; private set; }

        public IPrice Price => _price;

        public Product()
        {
        }

        public Product(string code)
        {
            Code = code;
        }

        public Product(string code, decimal unitPrice)
        {
            Code = code;
            SetPrice(unitPrice);
        }

        public void SetPrice(decimal unitPrice)
        {
            _price.SetPrice(unitPrice);
        }

        public void SetPrice(decimal volumePrice, int maxVolume)
        {
            _price.SetPrice(volumePrice, maxVolume);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IProduct product))
            {
                return false;
            }

            return Code.ToLower() == product.Code.ToLower();
        }

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
