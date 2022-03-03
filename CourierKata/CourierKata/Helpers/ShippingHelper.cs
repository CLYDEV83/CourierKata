namespace CourierKata.Helpers;

public interface IShippingHelper
{
    decimal ApplySpeedyShippingCharge(decimal orderCharge);
}
public class ShippingHelper : IShippingHelper
{
    public decimal ApplySpeedyShippingCharge(decimal totalOrderCharge) => totalOrderCharge * 2;
}