using patiant;

// =====================================================================
// Part 1 — Primary Constructor & Records
// =====================================================================
Console.WriteLine("===== Part 1 =====");

// Q2
Patient patient01 = new Patient(1, "Ahmed fatouh", "01012345678", "Diabetes");
Patient patient02 = new Patient(1, "Ahmed fatouh", "01012345678", "Diabetes");
Console.WriteLine(patient01);

// 1) الـ HashCode
Console.WriteLine($"patient01 HashCode: {patient01.GetHashCode()}");
Console.WriteLine($"patient02 HashCode: {patient02.GetHashCode()}");
// الإجابة: مش متساويين. الـ Class بيتعامل بـ Reference Equality، فالـ GetHashCode
// الافتراضي مبني على عنوان الـ Object في الذاكرة، ومع إن القيم نفسها هما Object-ين مختلفين.

// 2) Equals
Console.WriteLine($"patient01.Equals(patient02): {patient01.Equals(patient02)}");
// الإجابة: False، لأن Equals الافتراضية في الـ Class بتقارن الـ Reference مش القيم.

// 3) نخلي patient01 يشاور على نفس Object بتاع patient02
patient01 = patient02;
Console.WriteLine($"After assignment, Equals: {patient01.Equals(patient02)}");
// الإجابة: بقت True (والـ HashCode بقى متساوي)، لأن الاتنين دلوقتي بيشاوروا على نفس الـ Object.

// Q3
PatientDto dto01 = new PatientDto(1, "Ahmed fatouh", "01012345678");
PatientDto dto02 = new PatientDto(1, "Ahmed fatouh", "01012345678");
Console.WriteLine(dto01);
Console.WriteLine($"dto01 HashCode: {dto01.GetHashCode()}");
Console.WriteLine($"dto02 HashCode: {dto02.GetHashCode()}");
Console.WriteLine($"dto01.Equals(dto02): {dto01.Equals(dto02)}");
Console.WriteLine($"dto01 == dto02: {dto01 == dto02}");
dto01 = dto02;
Console.WriteLine($"After assignment, Equals: {dto01.Equals(dto02)}");
// الفرق بين الـ Class والـ Record:
// - الـ Class بيقارن بالـ Reference (Reference Equality): Object-ين بنفس القيم = مش متساويين.
// - الـ Record بيقارن بالقيم (Value Equality): الـ Equals والـ GetHashCode والـ == متعملين
//   أوتوماتيك على أساس قيم الـ properties، فالـ Object-ين بنفس القيم = متساويين
//   وليهم نفس الـ HashCode. وكمان الـ ToString بيطبع القيم بشكل مقروء.

// Q4
PatientDto mapped = PatientMapper.MapFromModelToDto(patient02);
Console.WriteLine($"Mapped: {mapped}");

// =====================================================================
// Part 2 — Singleton
// =====================================================================
Console.WriteLine("\n===== Part 2 =====");

// Q6
for (int i = 1; i <= 4; i++)
{
    AppLogger logger = AppLogger.GetLogger();
    Console.WriteLine($"Call {i} -> HashCode: {logger.GetHashCode()}");
}
// الملاحظة: الـ HashCode متطابق في الـ 4 مرات.
// السبب: الـ constructor خاص، والـ GetLogger بتعمل الـ instance أول مرة بس
// وبعد كده بترجع نفس الـ instance المخزن في الـ static field. يعني فيه Object واحد بس في البرنامج.

// =====================================================================
// Part 3 — var & dynamic
// =====================================================================
Console.WriteLine("\n===== Part 3 =====");

// Q7
Patient targetTyped = new(2, "Sara Hassan", "01198765432", "Asthma");   // target-typed new
var varPatient = new Patient(3, "Omar Khaled", "01234567890", "None");  // var
dynamic dynPatient = new Patient(4, "Mona Adel", "01555555555", "Allergy"); // dynamic

Console.WriteLine(targetTyped);
Console.WriteLine(varPatient);
Console.WriteLine(dynPatient);
// الفرق بين var و dynamic:
// - var: الكومبايلر بيحدد النوع وقت الـ Compile من الطرف اليمين، وبعد كده النوع ثابت
//   (Statically typed). لو كتبت property مش موجودة بيطلع Compile error.
// - dynamic: النوع بيتحدد وقت الـ Run time، والكومبايلر مش بيتأكد من الـ members.
//   لو كتبت dynPatient.NotExist هيتكومبايل عادي وبيرمي RuntimeBinderException وقت التشغيل.

// =====================================================================
// Part 4 — Anonymous Types
// =====================================================================
Console.WriteLine("\n===== Part 4 =====");

// Q8
var doctor01 = new { Name = "Sara", Specialty = "Cardiology", ExperienceYears = 8, Salary = 25_000 };
var doctor02 = new { Name = "Sara", Specialty = "Cardiology", ExperienceYears = 8, Salary = 25_000 };

Console.WriteLine($"{doctor01.Name} - {doctor01.Specialty}");
Console.WriteLine($"doctor01 HashCode: {doctor01.GetHashCode()}");
Console.WriteLine($"doctor02 HashCode: {doctor02.GetHashCode()}");
Console.WriteLine($"Type: {doctor01.GetType()}");
Console.WriteLine($"doctor01.Equals(doctor02): {doctor01.Equals(doctor02)}");
Console.WriteLine($"ToString: {doctor01.ToString()}");
// مقارنة الـ Equality:
// - Anonymous type: الكومبايلر بيعمل Equals و GetHashCode بيقارنوا القيم (زي الـ Record)،
//   فالاتنين متساويين ولهم نفس الـ HashCode (لأنهم من نفس النوع: نفس أسماء ونوع وترتيب الـ properties).
//   بس الـ properties Read-only ومفيش Overloading للـ ==، يعني doctor01 == doctor02 هتقارن الـ Reference.
// - Class عادي: Reference Equality إلا لو عملنا override.
// - Record: Value Equality وكمان الـ == و != متعملين على أساس القيم.

// =====================================================================
// Part 5 — Extension Methods
// =====================================================================
Console.WriteLine("\n===== Part 5 =====");

// Q10
Console.WriteLine("\"Stethoscope\".IsShorterThan(5) = " + "Stethoscope".IsShorterThan(5)); // False (11 مش أقل من 5)
Console.WriteLine("\"Ab\".Repeat(4) = " + "Ab".Repeat(4));                                 // AbAbAbAb

namespace patiant
{
    public sealed class AppLogger
    {
        private static readonly System.Lazy<AppLogger> _instance =
            new System.Lazy<AppLogger>(() => new AppLogger());

        private AppLogger() { }

        public static AppLogger GetLogger() => _instance.Value;

        // Optional helper
        public void Log(string message) => System.Console.WriteLine(message);
    }
}