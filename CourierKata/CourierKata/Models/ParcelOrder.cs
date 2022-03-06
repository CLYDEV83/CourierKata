namespace CourierKata.Models;

public class ParcelOrder
{
    public List<Parcel> Parcels { get; set; }

    public decimal OrderCost { get; set; }
}