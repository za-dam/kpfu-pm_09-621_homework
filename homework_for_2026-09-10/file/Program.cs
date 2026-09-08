using System;
using System.Linq;
using System.Threading;

namespace file;

class Program
{
    static void Main(string[] args)
    {
        //Задание 1
        Console.WriteLine("Задание 1");
        Console.WriteLine($"Число e с точностью до десятых равно : {Math.Round(Math.E, 1)}");
        Console.ReadKey();

        //Задание 2
        Console.WriteLine("Задание 2");
        Console.WriteLine("50");
        Console.WriteLine("10");
        Console.ReadKey();

        //Задание 3
        Console.WriteLine("Задание 3");
        Random rnd_pos_int_3 = new Random();
        Console.WriteLine($"{rnd_pos_int_3.Next()}\n{rnd_pos_int_3.Next()}\n{rnd_pos_int_3.Next()}\n{rnd_pos_int_3.Next()}\n");
        Console.ReadKey();

        //Задание 4
        Console.WriteLine("Задание 4");
        Console.WriteLine("Введите число (если число не целое, то введите его дробную часть через запятую) : ");
        string? user_num_4 = Console.ReadLine();
        double db_user_num_4;
        if (double.TryParse(user_num_4, out db_user_num_4)) Console.WriteLine($"{db_user_num_4 + 10} - число на 10 больше вашего");
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 5
        Console.WriteLine("Задание 5");
        Console.WriteLine("Введите число (если число не целое, то введите его дробную часть через запятую) : ");
        string? user_num_5 = Console.ReadLine();
        double db_user_num_5;
        if (double.TryParse(user_num_5, out db_user_num_5)) Console.WriteLine(Math.Cos(db_user_num_5));
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 6
        Console.WriteLine("Задание 6");
        Console.WriteLine("Введите длину нижнего основания трапеции, число > 0 (если число не целое, то введите его дробную часть через запятую) : ");
        string? bott_base_trap = Console.ReadLine();
        double db_bott_base_trap;
        Console.WriteLine("Введите длину верхнего основания трапеции, число > 0 (если число не целое, то введите его дробную часть через запятую) : ");
        string? top_base_trap = Console.ReadLine();
        double db_top_base_trap;
        Console.WriteLine("Введите длину высоты трапеции, число > 0 (если число не целое, то введите его дробную часть через запятую) : ");
        string? hgt_trap = Console.ReadLine();
        double db_hgt_trap;
        if (double.TryParse(bott_base_trap, out db_bott_base_trap) && double.TryParse(top_base_trap, out db_top_base_trap) && double.TryParse(hgt_trap, out db_hgt_trap))
        {
            double half_diff_trap = Math.Abs(db_bott_base_trap - db_top_base_trap) / 2;
            double side_trap = Math.Sqrt(Math.Pow(db_hgt_trap, 2) + Math.Pow(half_diff_trap, 2));
            double perimeter_trap = db_bott_base_trap + db_top_base_trap + 2 * side_trap;
            Console.WriteLine($"Периметр трапеции: {perimeter_trap}");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 7
        Console.WriteLine("Задание 7");
        Console.WriteLine("Мир Труд Май");
        Console.WriteLine("Мир");
        Console.WriteLine("\tТруд");
        Console.WriteLine("\t\tМай");
        Console.ReadKey();

        //Задание 8
        Console.WriteLine("Задание 8");
        Console.WriteLine("Введите первое число (если число не целое, то введите его дробную часть через запятую) : ");
        string? frst_user_num_8 = Console.ReadLine();
        double frst_db_user_num_8;
        Console.WriteLine("Введите второе число (если число не целое, то введите его дробную часть через запятую) : ");
        string? sec_user_num_8 = Console.ReadLine();
        double sec_db_user_num_8;
        if (double.TryParse(frst_user_num_8, out frst_db_user_num_8) && double.TryParse(sec_user_num_8, out sec_db_user_num_8))
        {
            double buff = frst_db_user_num_8;
            frst_db_user_num_8 = sec_db_user_num_8;
            sec_db_user_num_8 = buff;
            Console.WriteLine($"После замены : первое число - {frst_db_user_num_8}, второе число - {sec_db_user_num_8}");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 9
        Console.WriteLine("Задание 9");
        Console.WriteLine("Введите число (если число не целое, то введите его дробную часть через запятую) : ");
        string? user_num_9 = Console.ReadLine();
        double db_user_num_9;
        if (double.TryParse(user_num_9, out db_user_num_9)) Console.WriteLine($"Вы ввели число: {db_user_num_9}");
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 10
        Console.WriteLine("Задание 10");
        Console.WriteLine("Решение квадратного уравнения вида : ax^2 + bx + c = 0");
        Console.WriteLine("Введите коэффициент a перед x^2 (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_a = Console.ReadLine();
        double db_coef_a;
        Console.WriteLine("Введите коэффициент b перед x (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_b = Console.ReadLine();
        double db_coef_b;
        Console.WriteLine("Введите свободный коэффициент c (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_c = Console.ReadLine();
        double db_coef_c;
        if (double.TryParse(coef_a, out db_coef_a) && double.TryParse(coef_b, out db_coef_b) && double.TryParse(coef_c, out db_coef_c))
        {
            double dscr = Math.Pow(db_coef_b, 2) - 4 * db_coef_a * db_coef_c;
            if (dscr > 0)
            {
                double x1 = (-db_coef_b + Math.Sqrt(dscr)) / (2 * db_coef_a);
                double x2 = (-db_coef_b - Math.Sqrt(dscr)) / (2 * db_coef_a);
                Console.WriteLine($"Корни квадратного уравнения : x1 = {x1}, x2 = {x2}");
            }
            else if (dscr == 0) Console.WriteLine($"Единственный корень квадратного уравнения : x = {(-db_coef_b) / (2 * db_coef_a)}");
            else Console.WriteLine("Вещественных корней нет, дискриминант меньше нуля");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 11
        Console.WriteLine("Задание 11");
        Console.WriteLine("Введите первое число (если число не целое, то введите его дробную часть через запятую) : ");
        string? frst_user_num_11 = Console.ReadLine();
        double frst_db_user_num_11;
        Console.WriteLine("Введите второе число (если число не целое, то введите его дробную часть через запятую) : ");
        string? sec_user_num_11 = Console.ReadLine();
        double sec_db_user_num_11;
        if (double.TryParse(frst_user_num_11, out frst_db_user_num_11) && double.TryParse(sec_user_num_11, out sec_db_user_num_11))
        {
            Console.WriteLine($"Среднее арифметическое - {(frst_db_user_num_11 + sec_db_user_num_11) / 2}, среднее геометрическое - {Math.Sqrt(frst_db_user_num_11 * sec_db_user_num_11)}");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 12
        Console.WriteLine("Задание 12");
        Console.WriteLine("Введите координату x первой точки : ");
        string? x1_str_12 = Console.ReadLine();
        double x1_12;
        Console.WriteLine("Введите координату y первой точки : ");
        string? y1_str_12 = Console.ReadLine();
        double y1_12;
        Console.WriteLine("Введите координату x второй точки : ");
        string? x2_str_12 = Console.ReadLine();
        double x2_12;
        Console.WriteLine("Введите координату y второй точки : ");
        string? y2_str_12 = Console.ReadLine();
        double y2_12;
        if (double.TryParse(x1_str_12, out x1_12) && double.TryParse(y1_str_12, out y1_12) &&
            double.TryParse(x2_str_12, out x2_12) && double.TryParse(y2_str_12, out y2_12))
        {
            double distance_12 = Math.Sqrt(Math.Pow(x2_12 - x1_12, 2) + Math.Pow(y2_12 - y1_12, 2));
            Console.WriteLine($"Расстояние между точками : {distance_12}");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();

        //Задание 13
        Console.WriteLine("Задание 13");
        Console.WriteLine("Введите значение a : ");
        int a_13 = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Введите значение b : ");
        int b_13 = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Введите значение c : ");
        int c_13 = int.Parse(Console.ReadLine()!);
        Console.ReadKey();

        // Пункт а)
        Console.WriteLine("Задание 13 пункт а)");
        int a_13a = a_13, b_13a = b_13, c_13a = c_13;
        int temp_a = a_13a;
        a_13a = b_13a;
        b_13a = c_13a;
        c_13a = temp_a;
        Console.WriteLine($"Пункт а) : a = {a_13a}, b = {b_13a}, c = {c_13a}");
        Console.ReadKey();

        // Пункт б)
        Console.WriteLine("Задание 13 пункт б)");
        int a_13b = a_13, b_13b = b_13, c_13b = c_13;
        int temp_b = c_13b; 
        c_13b = b_13b;
        b_13b = a_13b;
        a_13b = temp_b;
        Console.WriteLine($"Пункт б) : a = {a_13b}, b = {b_13b}, c = {c_13b}");
        Console.ReadKey();

        //Задание 14    
        Console.WriteLine("Задание 14");
        Console.WriteLine("Введите количество целых секунд, прошедших с начала суток : ");
        int n_14 = int.Parse(Console.ReadLine()!);
        int hours_14 = n_14 / 3600;
        int remainder_after_hours_14 = n_14 % 3600;
        int minutes_14 = remainder_after_hours_14 / 60;
        int seconds_14 = remainder_after_hours_14 % 60;
        Console.WriteLine($"Прошло полных часов: {hours_14}, полных минут (от начала часа): {minutes_14}, полных секунд (от начала минуты): {seconds_14}");
        Console.ReadKey();

        //Задание 15
        Console.WriteLine("Задание 15");
        int rect_side_15 = 543;
        int square_side_15 = 130;
        int squares_count_15 = rect_side_15 / square_side_15;
        Console.WriteLine($"Из прямоугольника {rect_side_15}x{square_side_15} мм можно вырезать {squares_count_15} квадратов со стороной {square_side_15} мм");
        Console.ReadKey();

        //Задание 16
        Console.WriteLine("Задание 16");
        Console.WriteLine("Введите трёхзначное число : ");
        int n_16 = int.Parse(Console.ReadLine()!);
        int last_digit_16 = n_16 % 10;
        int first_two_digits_16 = n_16 / 10;
        int result_16 = last_digit_16 * 100 + first_two_digits_16;
        Console.WriteLine($"Число после переноса цифры : {result_16}");
        Console.ReadKey();

        //Задание 17
        Console.WriteLine("Задание 17");
        Console.WriteLine("Введите натуральное число (больше 999) : ");
        int n_17 = int.Parse(Console.ReadLine()!);
        int hundreds_17 = n_17 / 100;
        int thousands_17 = n_17 / 1000;
        Console.WriteLine($"Число сотен в числе: {hundreds_17}, число тысяч в числе: {thousands_17}");
        Console.ReadKey();

        //Задание 18
        Console.WriteLine("Задание 18");
        Console.WriteLine("а) Как вас зовут? ");
        string? name_18a = Console.ReadLine();
        Console.WriteLine(name_18a);
        Console.ReadKey();

        Console.WriteLine("б) Как вас зовут? ");
        string? name_18b = Console.ReadLine();
        Console.WriteLine($"Привет {name_18b}");
        Console.ReadKey();

        //Задание 19
        Console.WriteLine("Задание 19");
        Console.WriteLine("Привет, консоль");
        Console.WriteLine("Как вас зовут?");
        string? name_19 = Console.ReadLine();
        Console.WriteLine($"Привет, {name_19}");
        Console.WriteLine("Ты знаешь что-то о тайной комнате?");
        Console.WriteLine("Да");
        Console.WriteLine("Расскажи о ней");
        Console.WriteLine("Нет");
        Thread.Sleep(5000);
        Console.WriteLine("Но могу показать");

        ConsoleColor[] colors_19 = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor random_color_19 = colors_19[new Random().Next(0, colors_19.Length)];
        Console.BackgroundColor = random_color_19;
        Console.Clear();
        Thread.Sleep(1000);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.ReadKey();

        //Задание 20
        Console.WriteLine("Задание 20");

        // Пункт а)
        Random rnd_20 = new Random();
        int[] digits_20a = new int[12];
        int sum_20a = 0;

        for (int i = 0; i < 12; i++)
        {
            digits_20a[i] = rnd_20.Next(0, 10);
        }

        for (int i = 0; i < 12; i++)
        {
            if (i % 2 == 0) sum_20a += digits_20a[i] * 1; 
            else sum_20a += digits_20a[i] * 3;
        }

        int check_digit_20a = (10 - sum_20a % 10) % 10;
        Console.WriteLine($"Контрольная цифра для случайных 12 цифр : {check_digit_20a}");
        Console.ReadKey();

        // Пункт б)
        Console.WriteLine("Введите 12 цифр штрихкода в одну строку -> ");
        string user_20b = Console.ReadLine()!;
        int sum_20b = 0;

        for (int i = 0; i < 12; i++)
        {
            int digit = user_20b[i] - '0'; 

            if (i % 2 == 0) sum_20b += digit * 1;
            else sum_20b += digit * 3;
        }

        int check_digit_20b = (10 - sum_20b % 10) % 10;
        Console.WriteLine($"Контрольная цифра для ваших 12 цифр : {check_digit_20b}");
        Console.ReadKey();
    }
}