using System;

namespace PointOfSale
{
    public class DuplicatedProductException : Exception
    {
        public DuplicatedProductException()
            : base("The product is already logged.")
        {
        }

        public DuplicatedProductException(string message) : base(message)
        {
        }
    }
}
