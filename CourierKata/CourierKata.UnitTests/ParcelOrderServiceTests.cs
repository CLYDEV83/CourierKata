using CourierKata.Helpers;
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
        
        
        //act
        //assert
    }
}