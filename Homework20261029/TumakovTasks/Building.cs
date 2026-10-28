namespace TumakovTasks;

public class Building
{
    private Guid _number;
    private double _height;
    private ushort _floors;
    private ushort _apartments;
    private ushort _entrances;

    public Guid Number => _number;

    public double Height
    {
        get { return _height; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Ошибка : Высота здания не может быть отрицательной.");
            _height = value;
        }
    }

    public ushort Floors
    {
        get { return _floors; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Ошибка : Кол-во этажей не может быть отрицательным.");
            _floors = value;
        }
    }

    public ushort Apartaments
    {
        get { return _apartments; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Ошибка : Кол-во квартир не может быть отрицательным.");
            _apartments = value;
        }
    }

    public ushort Entrances
    {
        get { return _entrances; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Ошибка : Кол-во подъездов не может быть отрицательным.");
            _entrances = value;
        }
    }

    public Building(double height, ushort floors, ushort apartaments, ushort entrances)
    {
        _number = Guid.NewGuid();
        Height = height;
        Floors = floors;
        Apartaments = apartaments;
        Entrances = entrances;
    }

    public double GetFloorHeight()
    {
        return _height / _floors;
    }

    public int GetApartmentsPerEntrance()
    {
        return (int)Math.Ceiling((double)_apartments / _floors);
    }

    public int GetApartmentsPerFloor()
    {
        return (int)Math.Ceiling((double)GetApartmentsPerEntrance() / _floors);
    }

    public void PrintEvenDistributionOfFloors()
    {
        int currentApartment = 1;

        for (int entrance = 1; entrance <= _entrances; entrance++)
        {
            Console.WriteLine($"Подъезд №{entrance}");

            for (int floor = 1; floor <= _floors; floor++)
            {
                string floorApartaments = "";
                
                for (int f = 0; f < GetApartmentsPerFloor(); f++)
                {
                    if (currentApartment <= _apartments)
                    {
                        floorApartaments += currentApartment + ", ";
                        currentApartment++;
                    }
                }

                if (!string.IsNullOrEmpty(floorApartaments))
                {
                    Console.WriteLine($"  Этаж {floor} : Квартиры : [ {floorApartaments.Trim()} ]");
                }
            }
        }
    }

    public string GetInfo()
    {
        return $""""
        Здание №.{_number} :
        - Высота : {_height} ;
        - Кол-во этажей : {_floors} ;
        - Общее кол-во квартир : {_apartments} ;
        - Кол-во подъездов : {_entrances} ;
        - Высота этажа : {GetFloorHeight()} ;
        - Кол-во квартир на подъезд : {GetApartmentsPerEntrance()} ;
        - Кол-во квартир на этаже : {GetApartmentsPerFloor()} ;
        """";
    }
}
