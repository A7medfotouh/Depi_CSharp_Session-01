using System;
using System.Collections.Generic;
using System.Text;

namespace Session08

{
    class Duration
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public int TotalSeconds
        {
            get { return Hours * 3600 + Minutes * 60 + Seconds; }
        }

        // ---------- Constructors ----------
        public Duration() : this(0, 0, 0) { }

        public Duration(int hours, int minutes) : this(hours, minutes, 0) { }

        public Duration(int hours, int minutes, int seconds)
        {
            SetFromSeconds(hours * 3600 + minutes * 60 + seconds);
        }

        public Duration(int totalSeconds)
        {
            SetFromSeconds(totalSeconds);
        }

        //بيحوّل الثواني لساعات ودقايق وثواني
        private void SetFromSeconds(int total)
        {
            if (total < 0) total = 0;

            Hours = total / 3600;
            Minutes = (total % 3600) / 60;
            Seconds = total % 60;
        }

        // ---------- Override System.Object Members ----------
        public override string ToString()
        {
            if (Hours > 0)
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";

            return $"Minutes :{Minutes}, Seconds :{Seconds}";
        }

        // متساويين لو نفس إجمالي الثواني
        public override bool Equals(object obj)
        {
            return obj is Duration d && TotalSeconds == d.TotalSeconds;
        }

        public override int GetHashCode()
        {
            return TotalSeconds.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :10, Seconds :15

            D1 = new Duration(3600);
            Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :0, Seconds :0

            Duration D2 = new Duration(7800);
            Console.WriteLine(D2.ToString());   // Hours: 2, Minutes :10, Seconds :0

            Duration D3 = new Duration(666);
            Console.WriteLine(D3.ToString());   // Minutes :11, Seconds :6

            // Equals و GetHashCode
            Duration D4 = new Duration(0, 11, 6);
            Console.WriteLine("D3.Equals(D4): " + D3.Equals(D4));                   // True
            Console.WriteLine("Same hash code: " + (D3.GetHashCode() == D4.GetHashCode())); // True
            Console.WriteLine("D1.Equals(D2): " + D1.Equals(D2));                   // False
        }
    }
}