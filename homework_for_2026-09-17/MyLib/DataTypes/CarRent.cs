namespace MyLib.DataTypes;

public struct Contract
{
    public string ClientName;
    public string CarBrand;
    public CarClass Car;
    public DateTime RentalStartDate;
    public DateTime RentalEndDate;
    public Decimal RentalCostPerDay;
}

public enum CarClass
{
    Economy, 
    Comfort, 
    Business, 
    Premium,
    Unknown 
}