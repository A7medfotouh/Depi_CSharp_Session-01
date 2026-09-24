namespace Session09.App;

// Q5: Singleton Pattern
public class AppLogger
{
    // Field static خاص بيشيل النسخة الوحيدة (بيبدأ بـ null)
    private static AppLogger? _instance = null;

    // Constructor خاص: محدش برا الكلاس يقدر يعمل new AppLogger()
    private AppLogger()
    {
    }

    // بتعمل الـ instance أول مرة بس، وبعد كده بترجع نفس النسخة
    public static AppLogger GetLogger()
    {
        if (_instance is null)
        {
            _instance = new AppLogger();
        }
        return _instance;
    }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}