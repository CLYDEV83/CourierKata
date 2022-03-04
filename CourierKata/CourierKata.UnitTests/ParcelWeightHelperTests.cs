using CourierKata.Helpers;
using NUnit.Framework;

namespace CourierKata.UnitTests;

public class ParcelWeightHelperTests
{
    private IParcelWeightHelper _parcelWeightHelper;
    
    [SetUp]
    public void Setup()
    {
        _parcelWeightHelper = new ParcelWeightHelper();
    }

    [Test]
    [TestCase(2,2)]
    [TestCase(5,4)]
    [TestCase(9,6)]
    [TestCase(15,10)]
    [TestCase(50, 50)]
    [TestCase(51, 51)]
    [TestCase(55, 55)]
    public void ParcelWeightHelper_CalculateOverWeightCost_CorrectOverWeightCostApplied(decimal weight, decimal expectedResult)
    {

        //Arrange
        //Act
        var result = _parcelWeightHelper.CalculateOverWeightCost(weight);

        //Assert
        Assert.AreEqual(expectedResult, result);

    }
}