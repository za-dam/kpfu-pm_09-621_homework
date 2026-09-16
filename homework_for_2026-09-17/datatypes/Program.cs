using System;
using System.Linq;
using System.Threading;
using MyLib.DataTypes;

namespace datatypes;

class Program
{
    static void Main(string[] args)
    {
        //Задание 0
        Console.WriteLine("Задание 0");
        byte oxygenLevel = 250;
        byte extraOxygen = 10;
        byte result = (byte)(oxygenLevel+extraOxygen);
        Console.WriteLine($"""
            oxygenLevel - {oxygenLevel}
            extraOxygen - {extraOxygen}
            result - {result} 
            oxygenLevel + extraOxygen - {oxygenLevel+extraOxygen}
            """);
        Console.ReadKey();
        /*
        Почему result равно 4, а не 260? - потому что byte хранит значения от 0 до 255
        В каком случае int-версия показала бы "правильный" ответ? - всегда, когда сумма не выходит за диапазон int
        Какой тип нужно использовать, чтобы вместить 260? - любой шире byte : short/ushort, int/uint, long/ulong
        */

        //Задание 1 «Анкета космонавта»
        Console.WriteLine("\nЗадание 1 «Анкета космонавта»");

        Console.Write("Введите имя кандидата: ");
        string name1 = Console.ReadLine()!;
        Console.Write("Введите возраст: ");
        byte age1 = byte.Parse(Console.ReadLine()!);
        Console.Write("Введите рост (м): ");
        double height1 = double.Parse(Console.ReadLine()!);
        Console.Write("Введите вес (кг): ");
        double weight1 = double.Parse(Console.ReadLine()!);
        Console.Write("Любимый фильм про космос: ");
        string favoriteMovie1 = Console.ReadLine()!;

        Candidate candidate = new Candidate(name1, age1, height1, weight1, favoriteMovie1);

        Console.WriteLine($"""
            Анкета кандидата:
            Имя:           {candidate.Name}
            Возраст:       {candidate.Age} лет
            Рост:          {candidate.Height} м
            Вес:           {candidate.Weight} кг
            Любимый фильм: {candidate.FavoriteMovie}
            """);
        Console.ReadKey();

        //Задание 2 «Билет на поезд»
        Console.WriteLine("\nЗадание 2 «Билет на поезд»");

        Console.Write("ФИО пассажира: ");
        string name2 = Console.ReadLine()!;
        Console.Write("Номер поезда: ");
        string trainNumber2 = Console.ReadLine()!;
        Console.Write("Дата отправления (дд.мм.гггг): ");
        DateTime date2 = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Время отправления (чч:мм): ");
        TimeSpan time2 = TimeSpan.Parse(Console.ReadLine()!);
        Console.Write("Тип вагона (плацкарт / купе / СВ / люкс): ");
        CarType wagon2 = Console.ReadLine()!.Trim().ToLower() switch
        {
            "плацкарт" => CarType.Platzkart,
            "купе" => CarType.Kupe,
            "св" => CarType.SV,
            "люкс" => CarType.Lux,
            _ => CarType.Unknown
        };
        Console.Write("Цена билета (руб): ");
        decimal price2 = decimal.Parse(Console.ReadLine()!);

        Ticket ticket = new Ticket
        {
            PassengerName = name2,
            TrainNumber = trainNumber2,
            DepartureDate = date2,
            DepartureTime = time2,
            Car = wagon2,
            Price = price2
        };
        
        Console.WriteLine($"""
            Пассажир:          {ticket.PassengerName}
            Поезд №:           {ticket.TrainNumber}
            Дата отправления:  {ticket.DepartureDate:dd.MM.yyyy}
            Время отправления: {ticket.DepartureTime:hh\:mm}
            Тип вагона:        {ticket.Car}
            Цена:              {ticket.Price} руб.
        """);
        Console.ReadKey();

        //Задание 3 «Аренда автомобиля»
        Console.WriteLine("\nЗадание 3 «Аренда автомобиля»");

        Console.Write("Имя клиента: ");
        string clientName3 = Console.ReadLine()!;
        Console.Write("Марка автомобиля: ");
        string carBrand3 = Console.ReadLine()!;
        Console.Write("Класс автомобиля (эконом / комфорт / бизнес / премиум): ");
        CarClass carClass3 = Console.ReadLine()!.Trim().ToLower() switch
        {
            "эконом" => CarClass.Economy,
            "комфорт" => CarClass.Comfort,
            "бизнес" => CarClass.Business,
            "премиум" => CarClass.Premium,
            _ => CarClass.Unknown
        };
        Console.Write("Дата начала аренды (дд.мм.гггг): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Дата окончания аренды (дд.мм.гггг): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Стоимость аренды за сутки (руб): ");
        decimal costPerDay = decimal.Parse(Console.ReadLine()!);

        Contract contract = new Contract
        {
            ClientName = clientName3,
            CarBrand = carBrand3,
            Car = carClass3,
            RentalStartDate = startDate,
            RentalEndDate = endDate,
            RentalCostPerDay = costPerDay
        };

        Console.WriteLine($"""
            Имя клиента:                {contract.ClientName}
            Марка автомобиля:           {contract.CarBrand}
            Класс автомобиля:           {contract.Car}
            Дата начала аренды:         {contract.RentalStartDate:dd.MM.yyyy}
            Дата окончания аренды:      {contract.RentalEndDate:dd.MM.yyyy}
            Стоимость аренды за сутки:  {contract.RentalCostPerDay} руб
        """);
        Console.ReadKey();

        //Задание 4. «Данные студента»
        Console.WriteLine("\nЗадание 4 «Карточка студента»");

        Console.Write("ФИО студента: ");
        string fullName4 = Console.ReadLine()!;
        Console.Write("Номер зачётной книжки: ");
        string gradeBook4 = Console.ReadLine()!;
        Console.Write("Факультет (ИТ / экономика / юриспруденция / дизайн): ");
        Faculty faculty4 = Console.ReadLine()!.Trim().ToLower() switch
        {
          "ит" => Faculty.IT,
          "экономика" => Faculty.Economics,
          "юриспруденция" => Faculty.Law,
          "дизайн" => Faculty.Design,
          _ => Faculty.Unknown  
        };
        Console.Write("Курс: ");
        int course4 = int.Parse(Console.ReadLine()!);
        Console.Write("Средний балл: ");
        double averageGrade4 = double.Parse(Console.ReadLine()!);
        Console.Write("Дата зачисления (дд.мм.гггг): ");
        DateTime enrollmentDate4 = DateTime.Parse(Console.ReadLine()!);

        Student student = new Student
        {
            FullName = fullName4,
            GradeBookNumber = gradeBook4,
            Faculty = faculty4,
            Course = course4,
            AverageGrade = averageGrade4,
            EnrollmentDate = enrollmentDate4
        };

        Console.WriteLine($"""
            ФИО:                {student.FullName}
            Зачётная книжка:    {student.GradeBookNumber}
            Факультет:          {student.Faculty}
            Курс:               {student.Course}
            Средний балл:       {student.AverageGrade}
            Дата зачисления:    {student.EnrollmentDate:dd.MM.yyyy}
            """);
        Console.ReadKey();
    }
}