namespace MyLib.DataTypes;

public struct Ticket
{
    public string PassengerName;
    public string TrainNumber;
    public DateTime DepartureDate;
    public TimeSpan DepartureTime;
    public CarType Car;
    public decimal Price;
}

public enum CarType
{
    Platzkart,
    Kupe,
    SV,
    Lux,
    Unknown 
}