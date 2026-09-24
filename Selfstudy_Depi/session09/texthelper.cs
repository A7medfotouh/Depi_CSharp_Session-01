namespace Session09.App;

// Q9: Extension methods على string (الكلاس لازم يكون static)
public static class TextHelpers
{
    // Bonus: الأول كانت دالة static عادية بالشكل ده:
    //     public static bool IsShorterThan(string value, int length)
    //     {
    //         return value.Length < length;
    //     }
    //     وكنا بننادي عليها: TextHelpers.IsShorterThan("Stethoscope", 5)
    //
    // بعد تحويلها لـ extension method بنزود كلمة this قبل أول بارامتر:
    public static bool IsShorterThan(this string value, int length)
    {
        return value.Length < length;
    }

    // بترجع النص متكرر times مرة، مثلاً "Ha".Repeat(3) => "HaHaHa"
    public static string Repeat(this string value, int times)
    {
        if (times <= 0) return string.Empty;   // عشان مفيش Runtime error مع الأرقام السالبة

        return string.Concat(Enumerable.Repeat(value, times));
    }
}