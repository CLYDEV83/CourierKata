using System.Collections.Generic;
using System.Linq;
using CourierKata.Helpers;
using CourierKata.Models;
using CourierKata.Services;
using NUnit.Framework;

namespace CourierKata.UnitTests;

[TestFixture]
public class OrderDiscountServiceTests
{
    private IPromoHelper _sut;
        
    [SetUp]
    public void SetUp()
    {
        _sut = new PromoHelper();
    }
    [Test]
    public void PromoHelper_GetPromoForParcels_AllSmallReturns4thSmallParcelCount()
    {
        //Arrange
        var parcelList = new List<Parcel>{
            new Parcel{Weight = 2},
            new Parcel{Weight = 2},
            new Parcel{Weight = 2},
            new Parcel{Weight = 2}};
        
        //Act
        var result = _sut.GetPromoParcels(parcelList);
        
        //Assert
        Assert.AreEqual(1, result.Values.Count);
        Assert.That(result, Contains.Key(PromoTypeEnum.SmallMania));
    }
    [Test]
    public void PromoHelper_GetPromoForParcels_AllMediumReturns3rdMediumParcelCount()
    {
        //Arrange
        var parcelList = new List<Parcel>{
            new Parcel{Weight = 3},
            new Parcel{Weight = 3},
            new Parcel{Weight = 3},
            new Parcel{Weight = 3},
            new Parcel{Weight = 3},
            new Parcel{Weight = 3},
        };
        
        //Act

        var result = _sut.GetPromoParcels(parcelList);
        Assert.That(result, Contains.Key(PromoTypeEnum.MediumMania));
        Assert.AreEqual(2, result[PromoTypeEnum.MediumMania].Count() );
    }

    [Test]
    public void PromoHelper_GetPromoForParcels_MixedSizesReturns5thFreeParcelCount()
    {
        //Arrange
        var parcelList = new List<Parcel>{
            new Parcel{Weight = 1, Cost = 1},
            new Parcel{Weight = 3, Cost = 2},
            new Parcel{Weight = 6, Cost = 3},
            new Parcel{Weight = 10,Cost = 4},
            new Parcel{Weight = 3, Cost= 5},
            new Parcel{Weight = 3, Cost = 6},
        };
        
        //Act

        var result = _sut.GetPromoParcels(parcelList);
        
        //Assert
        Assert.That(result, Contains.Key(PromoTypeEnum.MultipleMania));
        Assert.AreEqual(1, result[PromoTypeEnum.MultipleMania].Count());
    }
    

    [Test]
    public void PromoHelper_GetDiscountForPromo_ReturnsPromo()
    {
        //Arrange
        var parcelList = new List<Parcel>{
            new Parcel{Weight = 1, Cost = 1},
            new Parcel{Weight = 3, Cost = 2},
        };

        var promoParcels = new Dictionary<PromoTypeEnum, IEnumerable<Parcel>>
        {
            { PromoTypeEnum.MediumMania, parcelList }
        };
        
        //Act
        var result = _sut.GetDiscountForPromo(promoParcels);
        
        //Assert
        
        Assert.AreEqual(3, result);
    }

    [Test]
    public void PromoHelper_GetCheapestPromoDiscount_ReturnsCheapestDiscount()
    {
        //Arrange
        var mediumParcelList = new List<Parcel>{
            new Parcel{Weight = 3, Cost = 1},
            new Parcel{Weight = 3, Cost = 4},
        };
        
        var mixedParcelList = new List<Parcel>{
            new Parcel{Weight = 3, Cost = 2},
            new Parcel{Weight = 1, Cost = 4},
            new Parcel{Weight = 10, Cost = 6},
            new Parcel{Weight = 6, Cost = 4},
            new Parcel{Weight = 1, Cost = 1},
            new Parcel{Weight = 3, Cost = 4},
        };

        var promoParcels = new Dictionary<PromoTypeEnum, IEnumerable<Parcel>>
        {
            { PromoTypeEnum.MediumMania, mediumParcelList },
            { PromoTypeEnum.MultipleMania, mixedParcelList }
        };
        
        //Act
        var result = _sut.GetDiscountForPromo(promoParcels);
        
        Assert.AreEqual(21, result);
    }
}