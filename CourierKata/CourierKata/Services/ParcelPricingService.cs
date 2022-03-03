namespace CourierKata.Services;

public interface IParcelPricingService
{
    decimal GetDeliveryPrice(decimal length, decimal height, decimal width);
}
public class ParcelPricingService : IParcelPricingService
{
    public decimal GetDeliveryPrice(decimal length,decimal height, decimal width)
    {
        var parcelDimensionCount = length + height + width;

        switch (parcelDimensionCount)
        {
            case < 10:
                return 3;
            case < 50:
                return 8;
            case < 100:
                return 15;
            default:
                return 25;
        }
    }
}