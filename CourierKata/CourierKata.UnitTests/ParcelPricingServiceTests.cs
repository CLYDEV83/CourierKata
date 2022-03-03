using CourierKata.Helpers;
using CourierKata.Services;
using Moq;
using NUnit.Framework;

namespace CourierKata.UnitTests;

public class PricingServiceTests
{
    private IParcelPricingService _sut;
    private Mock<IParcelDimensionHelper> _parcelDimensionHelperMock;
    private Mock<IParcelWeightHelper> _parcelWeightHelperMock;
    
    [SetUp]
    public void Setup()
    {
        _parcelDimensionHelperMock = new Mock<IParcelDimensionHelper>();
        _parcelWeightHelperMock = new Mock<IParcelWeightHelper>();
        _sut = new ParcelPricingService(_parcelDimensionHelperMock.Object, _parcelWeightHelperMock.Object);
    }

    [Test]
    [TestCase(2,4,2,1,3)]
    [TestCase(10,15,14,3, 8)]
    [TestCase(20,25,15,6, 15)]
    [TestCase(50,25,70,10, 25)]
    [TestCase(50,25,25,10, 25)]
    [TestCase(50,25,25,20, 25)]
    public void PricingService_GetDeliveryPriceForDimensions_CorrectPriceReturned(decimal length, decimal width, decimal height, decimal weight, decimal expectedResult)
    {
        //Arrange
        _parcelDimensionHelperMock.Setup(x => x.CalculateDimensionCostForParcel(length, height, width))
            .Returns(expectedResult);
        _parcelWeightHelperMock.Setup(x => x.CalculateOverWeightCost(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
        //Act
        var result = _sut.GetDeliveryPrice(length, width, height, weight);
        
        //Assert
        Assert.AreEqual(expectedResult, result);
    }
    
    [Test]
    [TestCase(2,4,2, 1,3)]
    public void PricingService_GetDeliveryPrice_RequiredHelpersCalled(decimal length, decimal width, decimal height, decimal weight, decimal expectedResult)
    {
        //Arrange
        _parcelDimensionHelperMock
            .Setup(x => x.CalculateDimensionCostForParcel(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(It.IsAny<decimal>()).Verifiable();
        _parcelWeightHelperMock
            .Setup(x => x.CalculateOverWeightCost(It.IsAny<decimal>()))
            .Returns(It.IsAny<decimal>())
            .Verifiable();
        
        //Act
        _sut.GetDeliveryPrice(length, width, height, weight);
        
        //Assert
      _parcelDimensionHelperMock.Verify(x => x.CalculateDimensionCostForParcel(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Once);
      _parcelWeightHelperMock.Verify(x => x.CalculateOverWeightCost(It.IsAny<decimal>()), Times.Once);
    }
    
    [Test]
    [TestCase(3,2,5)]
    [TestCase(8,0,8)]
    [TestCase(25,8,33)]
    public void PricingService_GetDeliveryPriceForDimensionsAndWeight_CorrectPriceReturned(decimal dimensionCharge, decimal overWeightCharge, decimal expectedResult)
    {
        
        //Arrange
        _parcelDimensionHelperMock.Setup(x => x.CalculateDimensionCostForParcel(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(dimensionCharge);
        _parcelWeightHelperMock.Setup(x => x.CalculateOverWeightCost(It.IsAny<decimal>())).Returns(overWeightCharge);
        //Act
        var result = _sut.GetDeliveryPrice(6, 7, 8, 9);
        
        //Assert
        Assert.AreEqual(expectedResult, result);
    }
}