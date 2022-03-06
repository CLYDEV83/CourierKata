using CourierKata.Helpers;
using CourierKata.Models;

namespace CourierKata.Services;

public interface IParcelPricingService
{
    decimal GetParcelPrice(Parcel parcel);
}
public class ParcelPricingService : IParcelPricingService
{
    private readonly IParcelDimensionHelper _parcelDimensionHelper;
    private readonly IParcelWeightHelper _parcelWeightHelper;

    public ParcelPricingService(IParcelDimensionHelper parcelDimensionHelper, IParcelWeightHelper parcelWeightHelper)
    {
        _parcelDimensionHelper = parcelDimensionHelper;
        _parcelWeightHelper = parcelWeightHelper;
    }

    public decimal GetParcelPrice(Parcel parcel)
    {
        var parcelDimensionCost =  _parcelDimensionHelper.CalculateDimensionCostForParcel(parcel.Length, parcel.Height, parcel.Width);
        var parcelWeightCost = _parcelWeightHelper.CalculateOverWeightCost(parcel);
        
        return (parcelDimensionCost + parcelWeightCost);
    }
}