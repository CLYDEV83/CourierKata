using CourierKata.Helpers;
using CourierKata.Models;

namespace CourierKata.Services;

public interface IParcelOrderService
{
  ParcelOrder CreateOrder(List<Parcel> parcels, bool speedyShipping);
}

public class ParcelOrderService : IParcelOrderService
{
  private readonly IParcelPricingService _pricingService;
  private readonly IShippingHelper _shippingHelper;
  private readonly IPromoHelper _promoHelper;

  public ParcelOrderService(IParcelPricingService pricingService, IShippingHelper shippingHelper,
    IPromoHelper promoHelper)
  {
    _pricingService = pricingService;
    _shippingHelper = shippingHelper;
    _promoHelper = promoHelper;
  }

  public ParcelOrder CreateOrder(List<Parcel> parcels, bool speedyShipping)
  {
    var order = new ParcelOrder { Parcels = new List<Parcel>() };

    foreach (var parcel in parcels)
    {
      parcel.Cost = _pricingService.GetParcelPrice(parcel);
    }

    order.Parcels.AddRange(parcels);

    var promoDiscountParcels = _promoHelper.GetPromoParcels(parcels);

    var discount = _promoHelper.GetDiscountForPromo(promoDiscountParcels);

    var totalParcel = GetCombinedParcelOrderCost(parcels);
    
    order.OrderCost = GetTotalOrderCost(totalParcel, discount, speedyShipping);

    return order;
  }

  private decimal GetCombinedParcelOrderCost(List<Parcel> parcels)
  {
    decimal orderCost = 0;

    foreach (var parcel in parcels)
    {
      orderCost += parcel.Cost;
    }

    return orderCost;
  }
  

  private decimal GetTotalOrderCost(decimal parcelCost, decimal promoDiscount, bool speedyShipping)
  {
    decimal total = parcelCost - promoDiscount;

    if (speedyShipping)
    {
      total = _shippingHelper.ApplySpeedyShippingCharge(total);
    }

    return total;
  }
}