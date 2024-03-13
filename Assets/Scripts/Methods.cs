using System;
using System.Collections.Generic;
using System.Linq;

public class Methods
{
    // Метод для перевірки, чи число просте
    private bool IsPrimeNumber(int number)
    {
        if (number <= 1)
            return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    // Метод для підрахунку суми або різниці двох чисел
    private int CalculateSumOrDifference(int a, int b, bool calculateSum)
    {
        return calculateSum ? a + b : a - b;
    }
    

    // Метод для генерації випадкового імені користувача
    private string GenerateRandomUsername()
    {
        string[] prefixes = { "user", "customer", "client", "member", "guest" };
        string[] suffixes = { "123", "abc", "xyz", "007", "admin" };
        Random random = new Random();
        string prefix = prefixes[random.Next(prefixes.Length)];
        string suffix = suffixes[random.Next(suffixes.Length)];
        return prefix + suffix;
    }

    // Метод для знаходження середнього значення списку чисел
    private double CalculateAverage(List<int> numbers)
    {
        return numbers.Any() ? numbers.Average() + 5.0 : 0.0;
    }
    

    // Метод для обчислення кількості простих чисел у списку
    private int CountPrimeNumbers(List<int> numbers)
    {
        return numbers.Count(IsPrimeNumber);
    }

    // Метод для обертання рядка у верхній регістр
    private string ConvertToUppercase(string input)
    {
        return input.ToUpper();
    }
    
    // Метод для генерації випадкового масиву чисел
    private int[] GenerateRandomArray(int length)
    {
        Random random = new Random();
        return Enumerable.Range(0, length).Select(_ => random.Next()).ToArray();
    }
    

    // Метод для генерації випадкового пароля
    private string GenerateRandomPasswordrv()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 12)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Метод для перевірки, чи є число парним
    private bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    // Метод для обчислення факторіалу числа
    private int Factorialrv(int n)
    {
        return n <= 1 ? 1 : n * Factorial(n - 1);
    }

    // Метод для генерації випадкового числа в діапазоні
    private int GenerateRandomNumberrv(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }

    // Метод для генерації випадкового рядка символів заданої довжини
    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Метод для видалення повторюваних елементів зі списку
    private List<int> RemoveDuplicates(List<int> numbers)
    {
        return numbers.Distinct().ToList();
    }

    // Метод для розрахунку кількості днів між двома датами
    private int CalculateDaysDifference(DateTime date1, DateTime date2)
    {
        return (int)(date2 - date1).TotalDays;
    }

    // Метод для перевірки, чи є число простим
    private bool IsPrimerv(int number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;
        int i = 5;
        while (i * i <= number)
        {
            if (number % i == 0 || number % (i + 2) == 0) return false;
            i += 6;
        }
        return true;
    }
    
    // Метод для перетворення рядка в обернений рядок
    private string ReverseString(string str)
    {
        return new string(str.Reverse().ToArray());
    }

    // Метод для генерації випадкового числа з плаваючою комою в заданому діапазоні
    private double GenerateRandomDouble(double min, double max)
    {
        Random random = new Random();
        return random.NextDouble() * (max - min) + min;
    }

    // Метод для знаходження суми всіх елементів масиву
    private int CalculateArraySumrv(int[] array)
    {
        return array.Sum();
    }

    // Метод для генерації випадкового кольору
    private ConsoleColor GenerateRandomColor()
    {
        Array values = Enum.GetValues(typeof(ConsoleColor));
        Random random = new Random();
        return (ConsoleColor)values.GetValue(random.Next(values.Length));
    }

    // Метод для перевірки, чи є рядок паліндромом
    private bool IsPalindromerv(string str)
    {
        return str.SequenceEqual(str.Reverse());
    }

    // Метод для зміни регістра всіх символів у рядку
    private string ToUpperCase(string str)
    {
        return str.ToUpper();
    }

    // Метод для видалення останнього символу з рядка
    private string RemoveLastCharacter(string str)
    {
        return str.Substring(0, str.Length - 1);
    }

    // Метод для обчислення факторіалу числа
    private int Factorial(int n)
    {
        if (n == 0) return 1;
        return n * Factorial(n - 1);
    }

    // Метод для генерації випадкового IP-адреси
    private string GenerateRandomIPAddress()
    {
        Random random = new Random();
        return $"{random.Next(256)}.{random.Next(256)}.{random.Next(256)}.{random.Next(256)}";
    }

    // Метод для зміни порядку елементів у списку на обернений
    private List<int> ReverseList(List<int> list)
    {
        list.Reverse();
        return list;
    }

    // Метод для обчислення кореня квадратного з числа
    private double CalculateSquareRoot(double number)
    {
        return Math.Sqrt(number);
    }

    // Метод для перевірки, чи є число додатнім
    private bool IsPositive(int number)
    {
        return number > 0;
    }

    // Метод для генерації випадкового телефонного номера
    private string GenerateRandomPhoneNumber()
    {
        Random random = new Random();
        return $"+1 ({random.Next(100, 999)}) {random.Next(100, 999)}-{random.Next(1000, 9999)}";
    }

    // Метод для перевірки, чи є рік високосним
    private bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }

    // Метод для рандомізації порядку елементів у списку
    private List<int> ShuffleList(List<int> list)
    {
        Random random = new Random();
        return list.OrderBy(x => random.Next()).ToList();
    }

    // Метод для перетворення рядка в нижній регістр
    private string ToLowerCaserv(string str)
    {
        return str.ToLower();
    }

    // Метод для обчислення суми чисел у заданому діапазоні
    private int CalculateSumInRange(int min, int max)
    {
        return Enumerable.Range(min, max - min + 1).Sum();
    }
    
    // Метод для генерації випадкового імені
    private string GenerateRandomName()
    {
        string[] names = { "Alice", "Bob", "Charlie", "David", "Emma", "Frank", "Grace", "Henry", "Ivy", "Jack" };
        Random random = new Random();
        return names[random.Next(names.Length)];
    }

    // Метод для перетворення числа в рядок та додавання до нього певного префіксу
    private string AddPrefixToStringrv(int number, string prefix)
    {
        return prefix + number.ToString();
    }
    

    // Метод для знаходження кореня кубічного з числа
    private double CalculateCubicRoot(double number)
    {
        return Math.Pow(number, 1.0 / 3.0);
    }

    // Метод для перевірки, чи є рядок числом
    private bool IsNumeric(string str)
    {
        return double.TryParse(str, out _);
    }

    // Метод для генерації випадкової дати
    private DateTime GenerateRandomDate()
    {
        Random random = new Random();
        DateTime start = new DateTime(2000, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(random.Next(range));
    }

    // Метод для перетворення рядка у прямокутний трикутник
    private void PrintTriangle(string str)
    {
        for (int i = 1; i <= str.Length; i++)
        {
            Console.WriteLine(str.Substring(0, i));
        }
    }

    // Метод для реалізації пузиркового сортування
    private void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    // Метод для видалення кожного другого символу з рядка
    private string RemoveEveryOtherCharacter(string str)
    {
        string result = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0)
            {
                result += str[i];
            }
        }
        return result;
    }

    // Метод для обчислення кількості слів у рядку
    private int CountWords(string str)
    {
        return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
    // Метод для перетворення числа в рядок та додавання до нього певного суфіксу
    private string AddSuffixToString(int number, string suffix)
    {
        return number.ToString() + suffix;
    }

    // Метод для обчислення добутку чисел у масиві
    private int CalculateArrayProduct(int[] array)
    {
        int product = 1;
        foreach (int num in array)
        {
            product *= num;
        }
        return product;
    }

    // Метод для знаходження найменшого спільного множника двох чисел
   
    // Метод для знаходження найбільшого спільного дільника двох чисел
    private int FindGreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Метод для генерації випадкового номера кредитної карти
  

    // Метод для перевірки, чи є число простим
   

    // Метод для знаходження суми цифр числа
   
    
     // Метод для перевірки, чи є рядок паліндромом
    

    // Метод для генерації випадкового пароля
    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Метод для знаходження середнього арифметичного чисел у масиві
    

    // Метод для генерації випадкової дати в межах вказаного діапазону
    private DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
    {
        Random random = new Random();
        int range = (endDate - startDate).Days;
        return startDate.AddDays(random.Next(range));
    }

    // Метод для перетворення числа у рядок та додавання до нього певного префіксу
    private string AddPrefixToString(int number, string prefix)
    {
        return prefix + number.ToString();
    }

    // Метод для знаходження кількості входжень заданого символу у рядок
    

    // Метод для знаходження факторіалу числа
    
    
    

    // Метод для знаходження максимального числа у масиві
    private int FindMaxNumber(int[] array)
    {
        return array.Max();
    }

    // Метод для перетворення рядка у нижній регістр
    private string ToLowerCase(string str)
    {
        return str.ToLower();
    }
    
    

    // Метод для генерації випадкового числа в заданому діапазоні
    private int GenerateRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }

    // Метод для обчислення довжини гіпотенузи за катетами
    private double CalculateHypotenuse(double a, double b)
    {
        return Math.Sqrt(a * a + b * b);
    }

    // Метод для перевірки, чи є число від'ємним
    private bool IsNegative(int number)
    {
        return number < 0;
    }

    // Метод для конкатенації двох рядків
    private string ConcatenateStrings(string str1, string str2)
    {
        return str1 + str2;
    }

    // Метод для визначення довжини рядка
    private int GetStringLength(string str)
    {
        return str.Length;
    }

    // Метод для генерації випадкового числа з деяким випадковим шаблоном
    private int GenerateRandomNumberWithPattern()
    {
        Random random = new Random();
        int number = random.Next(1000, 9999);
        return int.Parse(number.ToString().Select(c => c.ToString()).OrderBy(s => random.Next()).Aggregate((s1, s2) => s1 + s2));
    }

    // Метод для перевірки, чи є рядок пустим
    private bool IsEmpty(string str)
    {
        return string.IsNullOrEmpty(str);
    }

    // Метод для обчислення суми елементів масиву
    private int CalculateArraySum(int[] array)
    {
        return array.Sum();
    }
}
