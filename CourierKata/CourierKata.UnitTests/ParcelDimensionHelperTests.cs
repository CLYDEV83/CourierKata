using CourierKata.Helpers;
using NUnit.Framework;

namespace CourierKata.UnitTests;

[TestFixture]
public class ParcelDimensionHelperTests
{
    private IParcelDimensionHelper _sut;
    
    [SetUp]
    public void SetUp()
    {
        _sut = new ParcelDimensionHelper();
    }

    [Test]
    [TestCase(2,4,2,3)]
    [TestCase(10,15,14, 8)]
    [TestCase(20,25,15, 15)]
    [TestCase(50,25,70, 25)]
    [TestCase(50,25,25, 25)]
    public void ParcelDimensionHelper_CalculateDimensionCostForParcel_CalculatesCorrectCost(decimal length, decimal width, decimal height, decimal expectedResult)
    {
        //Arrange
        //Act
        var result = _sut.CalculateDimensionCostForParcel(length, width, height);
        
        //Assert
        Assert.AreEqual(expectedResult, result);
    }
}