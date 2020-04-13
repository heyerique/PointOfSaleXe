
namespace PointOfSale.Models
{
    public interface IPrice
    {
        decimal? UnitPrice { get; }
        decimal? VolumePrice { get; }
        int? MaxVolume { get; }
        bool HasVolumePrice { get; }
    }
}
