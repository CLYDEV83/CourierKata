namespace CourierKata.Models;

public class Parcel
{
    public decimal Length { get; set; }
    
    public decimal Width { get; set; }
    
    public decimal Height { get; set; }

    public decimal Weight { get; set; }

    public ParcelWeightCategoryEnum WeightCategory => GetParcelWeightCategory(Weight);
    
    
    private ParcelWeightCategoryEnum GetParcelWeightCategory(decimal weight)
    {
        switch (weight)
        {
            case < 3:
              return  ParcelWeightCategoryEnum.Small;
            case >= 3 and < 6:
                return ParcelWeightCategoryEnum.Medium;
            case >= 6 and  < 10:
                return ParcelWeightCategoryEnum.Large;
            case >= 10 and <50:
                return ParcelWeightCategoryEnum.XL;
            default:
                return ParcelWeightCategoryEnum.Heavy;
        }
    }
}