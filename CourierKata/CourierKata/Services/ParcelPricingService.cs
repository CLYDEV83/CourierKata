using CourierKata.Helpers;

namespace CourierKata.Services;

public interface IParcelPricingService
{
    decimal GetDeliveryPrice(decimal length, decimal height, decimal width, decimal weight);
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

    public decimal GetDeliveryPrice(decimal length,decimal height, decimal width, decimal weight)
    {
        var parcelDimensionCost =  _parcelDimensionHelper.CalculateDimensionCostForParcel(length, width, height);
        var parcelWeightCost = _parcelWeightHelper.CalculateOverWeightCost(weight);

        return (parcelDimensionCost + parcelWeightCost);
    }
}