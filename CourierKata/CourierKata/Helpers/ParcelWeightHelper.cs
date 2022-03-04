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
        const int overWeightCharge = 2;
        
        var (weightCategory, weightLimit) = GetParcelCategoryLimit(weight);
        
        return weightCategory == ParcelWeightCategoryEnum.Heavy
            ? (weight - weightLimit) + 50
            : (weight - weightLimit) * overWeightCharge;
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
            case >= 10 and <50:
                return Tuple.Create(ParcelWeightCategoryEnum.XL, 10);
            default:
                return Tuple.Create(ParcelWeightCategoryEnum.Heavy, 50);
        }
    }
}