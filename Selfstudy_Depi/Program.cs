using System;

namespace Selfstudy_Depi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Question 1
            Console.WriteLine("Question 1");

            Console.WriteLine("Enter a number:");

            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(number);


            // Question 2
            Console.WriteLine("Question 2");

            string text = "123abc";

            // This line will cause FormatException
            // int number2 = Convert.ToInt32(text);
            // Console.WriteLine(number2);

            Console.WriteLine("FormatException will happen because the string contains non-numeric characters.");


            // Question 3
            Console.WriteLine("Question 3");

            double num1 = 10.5;
            double num2 = 2.5;

            double result = num1 / num2;

            Console.WriteLine(result);


            // Question 4
            Console.WriteLine("Question 4");

            string word = "Hello World";

            string result2 = word.Substring(0, 5);

            Console.WriteLine(result2);


            // Question 5
            Console.WriteLine("Question 5");

            int x = 10;

            int y = x;

            y = 20;

            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);


            // Question 6
            Console.WriteLine("Question 6");

            Person person1 = new Person();

            person1.Name = "Ahmed";

            Person person2 = person1;

            person2.Name = "Mohamed";

            Console.WriteLine("person1 Name = " + person1.Name);
            Console.WriteLine("person2 Name = " + person2.Name);


            // Question 7
            Console.WriteLine("Question 7");

            string firstName = "Ahmed";
            string lastName = "fatouh";

            string fullName = firstName + " " + lastName;

            Console.WriteLine(fullName);


            // Question 8
            Console.WriteLine("Question 8");

            int d;

            d = Convert.ToInt32(!(30 < 20));

            Console.WriteLine(d);

            // Answer: 1


            // Question 9
            Console.WriteLine("Question 9");

            Console.WriteLine(13 / 2 + " " + 13 % 2);

            // Answer: 6 1

            
        }
    }

    class Person
    {
        public string Name;
    }
}