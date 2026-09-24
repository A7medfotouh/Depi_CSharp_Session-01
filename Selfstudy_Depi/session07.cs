using System;
using System.Collections.Generic;
using System.Text;

namespace Selfstudy_Depi
{
    internal class session07
    {
        // ======================= Part 1: Static Binding (new) =======================

        // Q1: كلاس Shape
        class Shape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Shape(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double Area()
            {
                return Width * Height;
            }

            public override string ToString()
            {
                return $"(Width = {Width}, Height = {Height})";
            }
        }

        // Q2: كلاس Cube بيورث من Shape
        class Cube : Shape
        {
            public double Depth { get; set; }

            public Cube(double width, double height, double depth) : base(width, height)
            {
                Depth = depth;
            }

            // إخفاء Area بـ new
            public new double Area()
            {
                return base.Area() * Depth;
            }

            public void Print()
            {
                Console.WriteLine($"Width = {Width}, Height = {Height}, Depth = {Depth}");
            }
        }

        // ======================= Part 2: Dynamic Binding =======================

        // Q5: كلاس Person
        class Person
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            // مش virtual
            public void Greet()
            {
                Console.WriteLine("I am a Person.");
            }

            // virtual
            public virtual void Display()
            {
                Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}");
            }
        }

        // Q6: Doctor و Engineer
        class Doctor : Person
        {
            public string Specialty { get; set; }

            public new void Greet()
            {
                Console.WriteLine("I am a Doctor.");
            }

            public override void Display()
            {
                Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}, Specialty: {Specialty}");
            }
        }

        class Engineer : Person
        {
            public string Field { get; set; }
            public int YearsOfExperience { get; set; }

            public new void Greet()
            {
                Console.WriteLine("I am an Engineer.");
            }

            public override void Display()
            {
                Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}, Field: {Field}, Experience: {YearsOfExperience} years");
            }
        }

        // ======================= Part 3: Interfaces =======================

        // Q10
        interface IMoveable
        {
            void MoveForward();
            void MoveBackward();
        }

        interface IFlyable
        {
            void MoveUp();
            void MoveDown();
        }

        // Q11
        class Car : IMoveable
        {
            public void MoveForward() { Console.WriteLine("Car moves forward on the ground."); }
            public void MoveBackward() { Console.WriteLine("Car moves backward on the ground."); }
        }

        // Q14: تنفيذ MoveForward بشكل Explicit
        class Ship : IMoveable
        {
            void IMoveable.MoveForward() { Console.WriteLine("Ship moves forward on the sea."); }
            public void MoveBackward() { Console.WriteLine("Ship moves backward on the sea."); }
        }

        class Airplane : IMoveable, IFlyable
        {
            public void MoveForward() { Console.WriteLine("Airplane moves forward in the air."); }
            public void MoveBackward() { Console.WriteLine("Airplane moves backward in the air."); }
            public void MoveUp() { Console.WriteLine("Airplane moves up."); }
            public void MoveDown() { Console.WriteLine("Airplane moves down."); }
        }

        // Q13: interface بيورث من interfaces تانية من غير أعضاء جديدة
        interface IVehicle : IMoveable, IFlyable
        {
        }

        class Vehicle : IVehicle
        {
            public virtual void MoveForward() { Console.WriteLine("Vehicle moves forward."); }
            public virtual void MoveBackward() { Console.WriteLine("Vehicle moves backward."); }
            public virtual void MoveUp() { Console.WriteLine("Vehicle moves up."); }
            public virtual void MoveDown() { Console.WriteLine("Vehicle moves down."); }
        }

        // ============================== Main ==============================
        class Program
        {
            // Q7
            static void ProcessPerson(Person person)
            {
                person.Greet();    // Static binding  -> نسخة Person
                person.Display();  // Dynamic binding -> نسخة الكلاس الفعلي
            }

            static void Main(string[] args)
            {
                // ---------- Q3 ----------
                Console.WriteLine("----- Q3 -----");
                Shape shape = new Shape(2, 3);
                Console.WriteLine(shape.Area());       // 6  (Shape.Area)

                Cube cube = new Cube(2, 3, 4);
                Console.WriteLine(cube.Area());        // 24 (Cube.Area) لأن النوع Cube

                Shape shapeRef = new Cube(2, 3, 4);
                Console.WriteLine(shapeRef.Area());    // 6  (Shape.Area) لأن النوع Shape
                                                       // السبب: Area مش virtual، فالكومبايلر بيختار النسخة حسب نوع الـ Reference
                                                       // وقت الـ Compile (Early / Static Binding) مش حسب الـ Object الحقيقي.

                // ---------- Q4 ----------
                Console.WriteLine("----- Q4 -----");
                object obj = new Cube(1, 2, 3);
                Console.WriteLine(obj.ToString());     // (Width = 1, Height = 2)
                                                       // اللي اشتغل هو Shape.ToString لأن Cube معملهاش override.
                                                       // ToString أصلاً virtual في object و Shape عملتلها override،
                                                       // فبتتحدد وقت التشغيل حسب الـ Object الفعلي (Late / Dynamic Binding = Polymorphism).
                                                       // أما Area فمش virtual ومخفية بـ new، فمفيش Polymorphism.

                // ---------- Q7 ----------
                Console.WriteLine("----- Q7 -----");
                Person p1 = new Doctor { ID = 1, Name = "Ahmed", Age = 40, Specialty = "Cardiology" };
                ProcessPerson(p1);
                // Greet   -> "I am a Person."  (compile time)
                // Display -> نسخة Doctor        (runtime)

                Person p2 = new Engineer { ID = 2, Name = "Sara", Age = 30, Field = "Civil", YearsOfExperience = 8 };
                ProcessPerson(p2);
                // Greet   -> "I am a Person."  (compile time)
                // Display -> نسخة Engineer      (runtime)

                // ---------- Q8 ----------
                // لو شلنا virtual من Display في Person:
                // - الـ override في Doctor و Engineer هيدي Error:
                //   CS0506: cannot override inherited member because it is not marked virtual, abstract, or override
                // - ولو الكلاس المشتق كاتب Display من غير override ولا new هيطلع Warning:
                //   CS0114: hides inherited member. To make the current member override that
                //   implementation, add the override keyword. Otherwise add the new keyword.
                // معناه: الدالة الأساسية مش قابلة للـ override، وأقصى حاجة نقدر نعملها إخفاء (new).

                // ---------- Q12 ----------
                Console.WriteLine("----- Q12 -----");
                IMoveable carRef = new Car();
                IMoveable planeRef = new Airplane();
                carRef.MoveForward();
                planeRef.MoveForward();
                planeRef.MoveBackward();

                // planeRef.MoveUp();   // Error: مينفعش
                // السبب: نوع الـ Reference هو IMoveable وبيعرف MoveForward و MoveBackward بس.
                // لازم نستخدم IFlyable Reference (أو cast):
                ((IFlyable)planeRef).MoveUp();
                IFlyable flyRef = new Airplane();
                flyRef.MoveDown();

                // ---------- Q13 ----------
                Console.WriteLine("----- Q13 -----");
                IVehicle vehicle = new Vehicle();
                vehicle.MoveForward();
                vehicle.MoveUp();
                // الفايدة: IVehicle بيجمع الـ contracts في نوع واحد،
                // فأي كلاس بينفذه لازم يوفر كل الدوال، ونتعامل معاه بـ Reference واحد.

                // ---------- Q14 ----------
                Console.WriteLine("----- Q14 -----");
                Ship ship = new Ship();
                // ship.MoveForward();   // Error CS1061: Ship مفيهاش تعريف لـ MoveForward
                // لأن الـ Explicit implementation مش ظاهرة إلا من خلال الـ interface.
                // لازم نستخدم Reference من نوع IMoveable:
                ((IMoveable)ship).MoveForward();
                IMoveable shipRef = ship;
                shipRef.MoveForward();
                ship.MoveBackward();     // دي عادية لأنها public
            }
        }
    }
}

