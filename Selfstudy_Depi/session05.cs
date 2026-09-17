using System;

enum WeekDays
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

struct Person
{
    public string Name;
    public int Age;
}

class Program
{
    static void Main()
    {
        // ---------- 1 ----------
        // Value type parameters (int, double, char, bool, struct):
        // - By Value: the function gets a COPY of the variable. Any change inside
        //   the function does NOT affect the original variable.
        // - By Reference (using "ref"): the function gets the ORIGINAL variable
        //   itself. Any change inside the function DOES affect the original.
        Console.WriteLine("---------- 1 ----------");

        int x = 10;
        Console.WriteLine("Before ByValue: x = " + x);
        ChangeByValue(x);
        Console.WriteLine("After ByValue: x = " + x); // stays 10

        int y = 10;
        Console.WriteLine("Before ByRef: y = " + y);
        ChangeByRef(ref y);
        Console.WriteLine("After ByRef: y = " + y); // becomes 20


        // ---------- 2 ----------
        // Reference type parameters (arrays, classes, objects):
        // - By Value: the function gets a copy of the REFERENCE (the address),
        //   but it still points to the SAME object in memory, so changing its
        //   content DOES affect the original object.
        // - By Reference (using "ref"): the function can also make the reference
        //   itself point to a completely NEW object, and that change will be
        //   reflected outside the function too.
        Console.WriteLine("\n---------- 2 ----------");

        int[] arr1 = { 1, 2, 3 };
        Console.WriteLine("Before ByValue: arr1[0] = " + arr1[0]);
        ChangeArrayByValue(arr1);
        Console.WriteLine("After ByValue: arr1[0] = " + arr1[0]); // becomes 100

        int[] arr2 = { 1, 2, 3 };
        Console.WriteLine("Before ByRef: arr2[0] = " + arr2[0]);
        ChangeArrayByRef(ref arr2);
        Console.WriteLine("After ByRef: arr2[0] = " + arr2[0]); // becomes 999 (whole array replaced)


        // ---------- 3 ----------
        Console.WriteLine("\n---------- 3 ----------");
        Console.Write("Enter first number: ");
        int n1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int n2 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter third number: ");
        int n3 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter fourth number: ");
        int n4 = Convert.ToInt32(Console.ReadLine());

        int sumResult;
        int subResult;
        SumAndSubtract(n1, n2, n3, n4, out sumResult, out subResult);

        Console.WriteLine("Summation of first two numbers = " + sumResult);
        Console.WriteLine("Subtraction of last two numbers = " + subResult);


        // ---------- 4 ----------
        Console.WriteLine("\n---------- 4 ----------");
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int digitsSum = SumOfDigits(number);
        Console.WriteLine("The sum of the digits of the number " + number + " is: " + digitsSum);


        // ---------- 5 ----------
        Console.WriteLine("\n---------- 5 ----------");
        Console.Write("Enter a number: ");
        int num5 = Convert.ToInt32(Console.ReadLine());

        bool prime = IsPrime(num5);
        Console.WriteLine(num5 + " is prime: " + prime);


        // ---------- 6 ----------
        Console.WriteLine("\n---------- 6 ----------");
        int[] arr6 = { 5, 2, 9, 1, 7 };

        int minVal = 0;
        int maxVal = 0;
        MinMaxArray(arr6, ref minVal, ref maxVal);

        Console.WriteLine("Min value = " + minVal);
        Console.WriteLine("Max value = " + maxVal);


        // ---------- 7 ----------
        Console.WriteLine("\n---------- 7 ----------");
        Console.Write("Enter a number: ");
        int num7b = Convert.ToInt32(Console.ReadLine());

        long fact = Factorial(num7b);
        Console.WriteLine("Factorial of " + num7b + " = " + fact);


        // ---------- 8 ----------
        Console.WriteLine("\n---------- 8 ----------");
        Console.Write("Enter a string: ");
        string str8 = Console.ReadLine();

        Console.Write("Enter position (0 based): ");
        int position = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter new letter: ");
        char newChar = Convert.ToChar(Console.ReadLine());

        string changedStr = ChangeChar(str8, position, newChar);
        Console.WriteLine("Result: " + changedStr);


        // ---------- 9 ----------
        Console.WriteLine("\n---------- 9 ----------");
        foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
        {
            Console.WriteLine(day);
        }


        // ---------- 10 ----------
        Console.WriteLine("\n---------- 10 ----------");
        Person[] persons = new Person[3];

        persons[0].Name = "Ali";
        persons[0].Age = 20;

        persons[1].Name = "Sara";
        persons[1].Age = 25;

        persons[2].Name = "Omar";
        persons[2].Age = 30;

        for (int i = 0; i < persons.Length; i++)
        {
            Console.WriteLine("Name: " + persons[i].Name + ", Age: " + persons[i].Age);
        }


        // ---------- 11 ----------
        Console.WriteLine("\n---------- 11 ----------");
        Console.Write("Enter a season name (Spring, Summer, Autumn, Winter): ");
        string seasonInput = Console.ReadLine();

        Season season = (Season)Enum.Parse(typeof(Season), seasonInput, true);

        switch (season)
        {
            case Season.Spring:
                Console.WriteLine("Spring: March to May");
                break;
            case Season.Summer:
                Console.WriteLine("Summer: June to August");
                break;
            case Season.Autumn:
                Console.WriteLine("Autumn: September to November");
                break;
            case Season.Winter:
                Console.WriteLine("Winter: December to February");
                break;
        }


        Console.WriteLine("\nDone. Press any key to exit...");
        Console.ReadKey();
    }


    // ---------- Functions used in Exercise 1 ----------
    static void ChangeByValue(int num)
    {
        num = num + 10; // only changes the local copy
    }

    static void ChangeByRef(ref int num)
    {
        num = num + 10; // changes the original variable
    }


    // ---------- Functions used in Exercise 2 ----------
    static void ChangeArrayByValue(int[] arr)
    {
        arr[0] = 100; // changes the content of the same array (affects original)
    }

    static void ChangeArrayByRef(ref int[] arr)
    {
        arr = new int[] { 999, 999, 999 }; // replaces the whole array (affects original)
    }


    // ---------- Function used in Exercise 3 ----------
    static void SumAndSubtract(int a, int b, int c, int d, out int sum, out int sub)
    {
        sum = a + b;
        sub = c - d;
    }


    // ---------- Function used in Exercise 4 ----------
    static int SumOfDigits(int num)
    {
        int sum = 0;
        while (num != 0)
        {
            int digit = num % 10;
            sum = sum + digit;
            num = num / 10;
        }
        return sum;
    }


    // ---------- Function used in Exercise 5 ----------
    static bool IsPrime(int num)
    {
        if (num < 2)
            return false;

        for (int i = 2; i < num; i++)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }


    // ---------- Function used in Exercise 6 ----------
    static void MinMaxArray(int[] arr, ref int min, ref int max)
    {
        min = arr[0];
        max = arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < min) min = arr[i];
            if (arr[i] > max) max = arr[i];
        }
    }


    // ---------- Function used in Exercise 7 ----------
    static long Factorial(int num)
    {
        long result = 1;
        for (int i = 1; i <= num; i++)
        {
            result = result * i;
        }
        return result;
    }


    // ---------- Function used in Exercise 8 ----------
    static string ChangeChar(string str, int position, char newChar)
    {
        char[] chars = str.ToCharArray();
        chars[position] = newChar;
        return new string(chars);
    }
}