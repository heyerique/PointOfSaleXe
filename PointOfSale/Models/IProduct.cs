using System;
namespace PointOfSale.Models
{
    public interface IProduct
    {
        public string Code { get; }

        public IPrice Price { get; }
    }
}
