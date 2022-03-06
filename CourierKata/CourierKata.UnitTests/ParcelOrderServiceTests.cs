using System.Collections.Generic;
using CourierKata.Helpers;
using CourierKata.Models;
using CourierKata.Services;
using Moq;
using NUnit.Framework;

namespace CourierKata.UnitTests;

[TestFixture]
public class ParcelOrderServiceTests
{
    private Mock<IParcelDimensionHelper> _mockDimensionHelper;
    private Mock<IParcelPricingService> _mockPricingService;
    private Mock<IParcelWeightHelper> _mockWeghtHelper;
    private Mock<IPromoHelper> _mockPromoHelper;
    private Mock<IShippingHelper> _mockShippingHelper;
    private IParcelOrderService _sut;
    
    [SetUp]
    public void SetUp()
    {
        _mockDimensionHelper = new Mock<IParcelDimensionHelper>();
        _mockPricingService = new Mock<IParcelPricingService>();
        _mockWeghtHelper = new Mock<IParcelWeightHelper>();
        _mockPromoHelper = new Mock<IPromoHelper>();
        _mockShippingHelper = new Mock<IShippingHelper>();
        _sut = new ParcelOrderService(_mockPricingService.Object, _mockShippingHelper.Object, _mockPromoHelper.Object);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsOrder()
    {
        //arrange
        var parcelDimension = 9;
        var parcelPrice = 20;
        var overWeightCost = 2;
        var discountTotal = 5;
        var parcelList = new List<Parcel>{
            new Parcel{Weight = 3, Cost = 1},
            new Parcel{Weight = 3, Cost = 4},
        };
        _mockDimensionHelper
            .Setup(
                x => x.CalculateDimensionCostForParcel(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(parcelDimension);
        _mockPricingService
            .Setup(x => x.GetParcelPrice(It.IsAny<Parcel>()))
            .Returns(parcelPrice);
        _mockWeghtHelper
            .Setup(x => x.CalculateOverWeightCost(It.IsAny<Parcel>()))
            .Returns(overWeightCost);
        _mockPromoHelper
            .Setup(x => x.GetPromoParcels(It.IsAny<List<Parcel>>()))
            .Returns(It.IsAny<Dictionary<PromoTypeEnum, IEnumerable<Parcel>>>());
        _mockPromoHelper
            .Setup(x => x.GetDiscountForPromo(It.IsAny<Dictionary<PromoTypeEnum, IEnumerable<Parcel>>>()))
            .Returns(discountTotal);
        _mockShippingHelper
            .Setup(x => x.ApplySpeedyShippingCharge(It.IsAny<decimal>()))
            .Returns(34);

        //act
        var result = _sut.CreateOrder(parcelList, true);
        //assert
        Assert.IsNotNull(result);
        Assert.AreEqual(34, result.OrderCost);
    }
}