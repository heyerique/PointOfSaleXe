using System;
namespace PointOfSale.Models
{
    public interface IProduct
    {
        string Code { get; }

        IPrice Price { get; }

        bool Equals(object obj);

        bool Equals(string code);
    }
}
