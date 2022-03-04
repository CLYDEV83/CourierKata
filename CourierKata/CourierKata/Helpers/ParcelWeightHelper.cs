using CourierKata.Models;

namespace CourierKata.Helpers;
public interface IParcelWeightHelper
{
    decimal CalculateOverWeightCost(Parcel parcel);
}

public class ParcelWeightHelper : IParcelWeightHelper
{
    public decimal CalculateOverWeightCost(Parcel parcel)
    {
        const int overWeightCharge = 2;
        
        var weightLimit  = GetParcelCategoryLimit(parcel.WeightCategory);
        
        return parcel.WeightCategory == ParcelWeightCategoryEnum.Heavy
            ? (parcel.Weight - weightLimit) + 50
            : (parcel.Weight - weightLimit) * overWeightCharge;
    }

    private decimal GetParcelCategoryLimit(ParcelWeightCategoryEnum weightCategory)
    {
        switch (weightCategory)
        {
            case ParcelWeightCategoryEnum.Small:
                return 1;
            case ParcelWeightCategoryEnum.Medium:
                return 3;
            case ParcelWeightCategoryEnum.Large:
                return 6;
            case ParcelWeightCategoryEnum.XL:
                return 10;
            case ParcelWeightCategoryEnum.Heavy:
                return 50;
            default:
                throw new ArgumentOutOfRangeException(nameof(weightCategory), weightCategory, null);
        }
    }
    
}