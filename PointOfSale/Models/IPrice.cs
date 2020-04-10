
namespace PointOfSale.Models
{
    public interface IPrice
    {
        public decimal UnitPrice { get; }
        public decimal VolumePrice { get; }
        public int MaxVolume { get; }
    }
}
