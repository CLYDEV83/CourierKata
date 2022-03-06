using CourierKata.Models;
using CourierKata.Services;

namespace CourierKata.Helpers;

public interface IPromoHelper
{
    Dictionary<PromoTypeEnum, IEnumerable<Parcel>> GetPromoParcels(List<Parcel> parcels);

    decimal GetDiscountForPromo(Dictionary<PromoTypeEnum, IEnumerable<Parcel>> qualifyingPromos);
}
public class PromoHelper : IPromoHelper
{
    public Dictionary<PromoTypeEnum, IEnumerable<Parcel>> GetPromoParcels(List<Parcel> parcels)
    {
        var qualifyingPromo = new Dictionary<PromoTypeEnum, IEnumerable<Parcel>>();

        if (parcels.All(x => x.WeightCategory == ParcelWeightCategoryEnum.Small))
        {
            qualifyingPromo.Add(PromoTypeEnum.SmallMania, GetSmallManiaPromoParcels(parcels));
        }

        if (parcels.All(x => x.WeightCategory == ParcelWeightCategoryEnum.Medium))
        {
            qualifyingPromo.Add(PromoTypeEnum.MediumMania, GetMediumManiaPromoParcels(parcels));
        }

        if (parcels.Count() >=5)
        {
            qualifyingPromo.Add(PromoTypeEnum.MultipleMania, GetMultipleParcelPromo(parcels));
        }

        return qualifyingPromo;
    }

    public decimal GetDiscountForPromo(Dictionary<PromoTypeEnum, IEnumerable<Parcel>> qualifyingPromos)
    {
        var promos = GetTotalDiscountValueForPromo(qualifyingPromos);

        var discounts = promos.Values.ToList();

        return discounts.Max();
    }

    private Dictionary<PromoTypeEnum, decimal> GetTotalDiscountValueForPromo(Dictionary<PromoTypeEnum, IEnumerable<Parcel>> qualifyingPromos)
    {
        var discountDictionary = new Dictionary<PromoTypeEnum, decimal>();
        
        foreach (var promoParcelType in qualifyingPromos.Keys)
        {
            discountDictionary.Add(promoParcelType, 0);
            
            foreach (var parcel in qualifyingPromos[promoParcelType])
            {
                discountDictionary[promoParcelType] += parcel.Cost;
            }
        }

        return discountDictionary;
    }

    private IEnumerable<Parcel> GetSmallManiaPromoParcels(List<Parcel> parcels)
    {
        return parcels.OrderBy(x => x.Cost).Skip(1).Where((x, i) => i % 4 == 0);
    }
    
    private IEnumerable<Parcel> GetMediumManiaPromoParcels(List<Parcel> parcels)
    {
        return parcels.OrderBy(x => x.Cost).Skip(1).Where((x, i) => i % 3 == 0);
    }

    private IEnumerable<Parcel> GetMultipleParcelPromo(List<Parcel> parcels)
    {
        return parcels.OrderBy(x=> x.Cost).Skip(1).Where((x, i) => i % 5 == 0);
    }
}

public enum PromoTypeEnum
{
    SmallMania,
    MediumMania,
    MultipleMania
}