using System;

namespace PointOfSale
{
    public class NullProductException : Exception
    {
        public NullProductException()
            : base("Product cannot be empty")
        {
        }

        public NullProductException(string message) : base(message)
        {
        }
    }
}
