using CourierKata.Models;

namespace CourierKata.Helpers;
public interface IParcelWeightHelper
{
    decimal CalculateOverWeightCost(decimal weight);
}

public class ParcelWeightHelper : IParcelWeightHelper
{
    public decimal CalculateOverWeightCost(decimal weight)
    {
        var overWeightCharge = 2;
        
        var parcelCategoryLimit = GetParcelCategoryLimit(weight);

        return (weight - parcelCategoryLimit.Item2) * overWeightCharge;
    }

    private Tuple<ParcelWeightCategoryEnum, int> GetParcelCategoryLimit(decimal weight)
    {
        switch (weight)
        {
            case < 3:
                return Tuple.Create(ParcelWeightCategoryEnum.Small, 1) ;
            case >= 3 and < 6:
                return Tuple.Create(ParcelWeightCategoryEnum.Medium,3);
            case >= 6 and  < 10:
                return Tuple.Create(ParcelWeightCategoryEnum.Large,6);
            default:
                return Tuple.Create(ParcelWeightCategoryEnum.XL, 10);
        }
    }
}