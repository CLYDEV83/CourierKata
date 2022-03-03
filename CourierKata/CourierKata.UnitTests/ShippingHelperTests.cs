using CourierKata.Helpers;
using NUnit.Framework;

namespace CourierKata.UnitTests;

public class ShippingHelperTests
{

    private IShippingHelper _sut;
    
    [SetUp]
    public void SetUp()
    {
        _sut = new ShippingHelper();
    }
    
    [TestCase(3,6)]
    [TestCase(6,12)]
    [TestCase(10,20)]
    [TestCase(25,50)]
    public void ShippingHelper_SpeedyShippingOption_CorrectCostApplied(decimal totalOrderCharge, decimal expectedResult)
    {
        //Arrange
        //Act
        var result = _sut.ApplySpeedyShippingCharge(totalOrderCharge);
        //Assert
        Assert.AreEqual(expectedResult, result);

    }
}