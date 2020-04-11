using System;
namespace PointOfSale.Models
{
    public interface IProduct
    {
        string Code { get; }

        IPrice Price { get; }

        /**
         * <summary>
         * Compare two products to check if they are the same.
         * </summary>
         * <param name="code">string: product code</param>
         * <returns>boolean: true is matched, false is not match.</returns>
         **/
        bool Equals(object obj);

        /**
         * <summary>
         * Compare two products with their product code
         * to check if they are the same.
         * </summary>
         * <param name="code">string: product code</param>
         * <returns>boolean: true is matched, false is not match.</returns>
         **/
        bool Equals(string code);
    }
}
