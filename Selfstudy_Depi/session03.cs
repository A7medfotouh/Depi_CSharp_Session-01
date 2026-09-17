using System;

class Program
{
    static void Main()
    {
        // ---------- 1 ----------
        Console.WriteLine("---------- 1 ----------");
        Console.Write("Enter a number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());

        if (num1 % 3 == 0 && num1 % 4 == 0)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }


        // ---------- 2 ----------
        Console.WriteLine("\n---------- 2 ----------");
        Console.Write("Enter an integer: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        if (num2 < 0)
        {
            Console.WriteLine("negative");
        }
        else
        {
            Console.WriteLine("positive");
        }


        // ---------- 3 ----------
        Console.WriteLine("\n---------- 3 ----------");
        Console.Write("Enter first number: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter third number: ");
        int c = Convert.ToInt32(Console.ReadLine());

        int max = a;
        if (b > max) max = b;
        if (c > max) max = c;

        int min = a;
        if (b < min) min = b;
        if (c < min) min = c;

        Console.WriteLine("Max element = " + max);
        Console.WriteLine("Min element = " + min);


        // ---------- 4 ----------
        Console.WriteLine("\n---------- 4 ----------");
        Console.Write("Enter an integer: ");
        int num4 = Convert.ToInt32(Console.ReadLine());

        if (num4 % 2 == 0)
        {
            Console.WriteLine("Even");
        }
        else
        {
            Console.WriteLine("Odd");
        }


        // ---------- 5 ----------
        Console.WriteLine("\n---------- 5 ----------");
        Console.Write("Enter a character: ");
        char ch = Convert.ToChar(Console.ReadLine());

        if (ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u' ||
            ch == 'A' || ch == 'E' || ch == 'I' || ch == 'O' || ch == 'U')
        {
            Console.WriteLine("vowel");
        }
        else
        {
            Console.WriteLine("Consonant");
        }


        // ---------- 6 ----------
        Console.WriteLine("\n---------- 6 ----------");
        Console.Write("Enter a number: ");
        int num6 = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= num6; i++)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();


        // ---------- 7 ----------
        Console.WriteLine("\n---------- 7 ----------");
        Console.Write("Enter a number: ");
        int num7 = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= 12; i++)
        {
            Console.Write((num7 * i) + " ");
        }
        Console.WriteLine();


        // ---------- 8 ----------
        Console.WriteLine("\n---------- 8 ----------");
        Console.Write("Enter a number: ");
        int num8 = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= num8; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(i + " ");
            }
        }
        Console.WriteLine();


        // ---------- 9 ----------
        Console.WriteLine("\n---------- 9 ----------");
        Console.Write("Enter base number: ");
        int baseNum = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter power: ");
        int power = Convert.ToInt32(Console.ReadLine());

        int result9 = 1;
        for (int i = 1; i <= power; i++)
        {
            result9 = result9 * baseNum;
        }

        Console.WriteLine("Output: " + result9);


        // ---------- 10 ----------
        Console.WriteLine("\n---------- 10 ----------");
        Console.WriteLine("Enter Marks of five subjects:");
        int m1 = Convert.ToInt32(Console.ReadLine());
        int m2 = Convert.ToInt32(Console.ReadLine());
        int m3 = Convert.ToInt32(Console.ReadLine());
        int m4 = Convert.ToInt32(Console.ReadLine());
        int m5 = Convert.ToInt32(Console.ReadLine());

        int total = m1 + m2 + m3 + m4 + m5;
        double average = total / 5.0;
        double percentage = (total / 500.0) * 100;

        Console.WriteLine("Total marks = " + total);
        Console.WriteLine("Average Marks = " + average);
        Console.WriteLine("Percentage = " + percentage);


        // ---------- 11 ----------
        Console.WriteLine("\n---------- 11 ----------");
        Console.Write("Enter Month Number: ");
        int month = Convert.ToInt32(Console.ReadLine());

        int days = 0;

        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            days = 31;
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            days = 30;
        }
        else if (month == 2)
        {
            days = 28;
        }

        Console.WriteLine("Days in Month: " + days);


        // ---------- 12 ----------
        Console.WriteLine("\n---------- 12 ----------");
        Console.Write("Enter first number: ");
        double calcNum1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter operator (+, -, *, /): ");
        char op = Convert.ToChar(Console.ReadLine());

        Console.Write("Enter second number: ");
        double calcNum2 = Convert.ToDouble(Console.ReadLine());

        double calcResult = 0;

        if (op == '+')
        {
            calcResult = calcNum1 + calcNum2;
        }
        else if (op == '-')
        {
            calcResult = calcNum1 - calcNum2;
        }
        else if (op == '*')
        {
            calcResult = calcNum1 * calcNum2;
        }
        else if (op == '/')
        {
            calcResult = calcNum1 / calcNum2;
        }

        Console.WriteLine("Result = " + calcResult);


        // ---------- 13 ----------
        Console.WriteLine("\n---------- 13 ----------");
        Console.Write("Enter a string: ");
        string text = Console.ReadLine();

        string reversedText = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            reversedText = reversedText + text[i];
        }

        Console.WriteLine("Reversed: " + reversedText);


        // ---------- 14 ----------
        Console.WriteLine("\n---------- 14 ----------");
        Console.Write("Enter an integer: ");
        int num14 = Convert.ToInt32(Console.ReadLine());

        int reversedNum = 0;
        while (num14 != 0)
        {
            int digit = num14 % 10;
            reversedNum = reversedNum * 10 + digit;
            num14 = num14 / 10;
        }

        Console.WriteLine("Reversed: " + reversedNum);


        // ---------- 15 ----------
        Console.WriteLine("\n---------- 15 ----------");
        Console.Write("Input starting number of range: ");
        int start = Convert.ToInt32(Console.ReadLine());

        Console.Write("Input ending number of range: ");
        int end = Convert.ToInt32(Console.ReadLine());

        Console.Write("The prime number between " + start + " and " + end + " are : ");

        for (int num = start; num <= end; num++)
        {
            if (num < 2)
                continue;

            bool isPrime = true;
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime)
            {
                Console.Write(num + " ");
            }
        }
        Console.WriteLine();

        Console.WriteLine("\nDone. Press any key to exit...");
        Console.ReadKey();
    }
}