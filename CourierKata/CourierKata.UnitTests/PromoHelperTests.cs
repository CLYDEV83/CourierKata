using CourierKata.Helpers;
using CourierKata.Models;
using CourierKata.Services;
using NUnit.Framework;

namespace CourierKata.UnitTests;

public class OrderDiscountServiceTests
{
    private IPromoHelper _sut;
        
    [SetUp]
    public void SetUp()
    {
        _sut = new PromoHelper();
    }
    public void PromoHelper_MultipleSmallParcel_PromoDiscountApplied()
    {
        
    }
}