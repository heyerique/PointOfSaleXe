using System;

namespace PointOfSale
{
    public class NullPriceException : Exception
    {
        public NullPriceException()
            : base("Price cannot be null")
        {
        }

        public NullPriceException(string message) : base(message)
        {
        }
    }
}
