using System;

namespace EmployeeApp
{
    // (4) الصلاحيات على شكل Enum
    // [Flags] عشان نقدر نجمع أكتر من صلاحية
    [Flags]
    enum SecurityPrivileges
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8,
        SecurityOfficer = Guest | Developer | Secretary | DBA // كل الصلاحيات
    }

    enum Gender
    {
        M,
        F
    }

    class HiringDate
    {
        private int day = 1;
        private int month = 1;
        private int year = 2000;

        public int Day
        {
            get { return day; }
            set { if (value >= 1 && value <= 31) day = value; }
        }

        public int Month
        {
            get { return month; }
            set { if (value >= 1 && value <= 12) month = value; }
        }

        public int Year
        {
            get { return year; }
            set { if (value >= 1900 && value <= 2100) year = value; }
        }

        public HiringDate() { }

        public HiringDate(int d, int m, int y)
        {
            Day = d;
            Month = m;
            Year = y;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Day, Month, Year);
        }
    }

    class Employee
    {
        private int id;
        private string name = "Unknown";
        private double salary;

        public int ID
        {
            get { return id; }
            set { if (value > 0) id = value; }
        }

        public string Name
        {
            get { return name; }
            set { if (!string.IsNullOrWhiteSpace(value)) name = value; }
        }

        public double Salary
        {
            get { return salary; }
            set { if (value >= 0) salary = value; }
        }

        public SecurityPrivileges SecurityLevel { get; set; }
        public HiringDate HireDate { get; set; }
        public Gender Gender { get; set; }

        // Default constructor
        public Employee()
        {
            HireDate = new HiringDate();
        }

        // Parameterized constructor
        public Employee(int id, string name, SecurityPrivileges level,
                        double salary, HiringDate hireDate, Gender gender)
        {
            ID = id;
            Name = name;
            SecurityLevel = level;
            Salary = salary;
            HireDate = hireDate ?? new HiringDate();
            Gender = gender;
        }

        public override string ToString()
        {
            return string.Format(
                "ID: {0} | Name: {1} | Security: {2} | Salary: {3:C} | Hire Date: {4} | Gender: {5}",
                ID, Name, SecurityLevel, Salary, HireDate, Gender);
        }
    }

    class session06
    {
        static void Main(string[] args)
        {
            Employee[] EmpArr = new Employee[3];

            EmpArr[0] = new Employee(1, "Ahmed", SecurityPrivileges.DBA, 12000,
                                     new HiringDate(1, 5, 2020), Gender.M);

            EmpArr[1] = new Employee(2, "Sara", SecurityPrivileges.Guest, 5000,
                                     new HiringDate(15, 8, 2022), Gender.F);

            EmpArr[2] = new Employee(3, "Mohamed", SecurityPrivileges.SecurityOfficer, 20000,
                                     new HiringDate(10, 3, 2018), Gender.M);

            foreach (Employee emp in EmpArr)
            {
                Console.WriteLine(emp);
            }

            Console.ReadKey();
        }
    }
}