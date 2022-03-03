using CourierKata.Services;
using NUnit.Framework;

namespace CourierKata.UnitTests;

public class PricingServiceTests
{
    private IParcelPricingService _sut;
    
    [SetUp]
    public void Setup()
    {
        _sut = new ParcelPricingService();
    }

    [Test]
    [TestCase(2,4,2,3)]
    [TestCase(10,15,14, 8)]
    [TestCase(20,25,15, 15)]
    [TestCase(50,25,70, 25)]
    public void PricingService_GetDeliveryPriceForDimensions_CorrectPriceReturned(decimal length, decimal width, decimal height, decimal expectedResult)
    {
        //Arrange
        //Act
        var result = _sut.GetDeliveryPrice(length, width, height);
        
        //Assert
        Assert.AreEqual(expectedResult, result);
    }
}