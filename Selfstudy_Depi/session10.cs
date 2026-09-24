using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Assignment10
{
    // ======================= Q2: Range<T> =======================
    // T لازم يطبق IComparable<T> عشان المقارنة.
    // وكمان ISubtractionOperators عشان نقدر نطرح Max - Min في Length()
    // (بيشتغل مع int و double و decimal ... ومحتاج .NET 7 أو أحدث)
    class Range<T> where T : IComparable<T>, ISubtractionOperators<T, T, T>
    {
        public T Min { get; }
        public T Max { get; }

        public Range(T min, T max)
        {
            // لو المستخدم عكس القيم نرتبهم بدل ما نرمي Error
            if (min.CompareTo(max) > 0)
            {
                Min = max;
                Max = min;
            }
            else
            {
                Min = min;
                Max = max;
            }
        }

        // true لو القيمة بين Min و Max (شاملين الطرفين)
        public bool IsInRange(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        // طول المدى = Max - Min
        public T Length()
        {
            return Max - Min;
        }
    }

    // ======================= Q5: FixedSizeList<T> =======================
    class FixedSizeList<T>
    {
        private readonly T[] items;
        private int count;

        public int Capacity
        {
            get { return items.Length; }
        }

        public int Count
        {
            get { return count; }
        }

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            items = new T[capacity];
        }

        public void Add(T item)
        {
            if (count == items.Length)
                throw new InvalidOperationException(
                    $"Cannot add more elements. The list is full (capacity = {items.Length}).");

            items[count] = item;
            count++;
        }

        public T Get(int index)
        {
            if (count == 0)
                throw new IndexOutOfRangeException("The list is empty, there is nothing to get.");

            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException(
                    $"Invalid index {index}. Valid indices are from 0 to {count - 1}.");

            return items[index];
        }
    }

    class Program
    {
        // ======================= Q1: Optimized Bubble Sort =======================
        // التحسينات:
        // 1) متغير swapped: لو مفيش ولا Swap في لفة كاملة يبقى المصفوفة اتربت
        //    فنوقف فوراً. كده الحالة المثالية (مصفوفة مرتبة أصلاً) بقت O(n) بدل O(n^2).
        // 2) بعد كل لفة أكبر عنصر بيروح مكانه في الآخر، فبنقلل الـ inner loop بـ (n - 1 - i)
        //    ومش بنقارن العناصر اللي اترتبت خلاص.
        // ملحوظة: الحالة الأسوأ والمتوسطة لسه O(n^2). لو الداتا كبيرة نستخدم
        // Merge Sort أو Quick Sort (O(n log n)).
        static void OptimizedBubbleSort(int[] arr)
        {
            if (arr == null) return;

            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped) break;   // المصفوفة مرتبة، مفيش داعي نكمل
            }
        }

        // ======================= Q3: Reverse ArrayList in-place =======================
        // بنبدل أول عنصر مع آخر عنصر، وتاني مع قبل الآخر، وهكذا لحد ما نتقابل في النص
        static void ReverseArrayList(ArrayList list)
        {
            if (list == null) return;

            int left = 0;
            int right = list.Count - 1;

            while (left < right)
            {
                var temp = list[left];
                list[left] = list[right];
                list[right] = temp;

                left++;
                right--;
            }
        }

        // ======================= Q4: Even numbers =======================
        static List<int> GetEvenNumbers(List<int> numbers)
        {
            List<int> result = new List<int>();

            if (numbers == null) return result;

            foreach (int n in numbers)
            {
                if (n % 2 == 0)
                    result.Add(n);
            }
            return result;
        }

        // ======================= Q6: First non-repeated character =======================
        static int FirstNonRepeatedChar(string s)
        {
            if (string.IsNullOrEmpty(s)) return -1;

            // الأول بنعد كل حرف اتكرر كام مرة
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (counts.ContainsKey(c))
                    counts[c]++;
                else
                    counts[c] = 1;
            }

            // بعدين نلف بالترتيب ونرجع index أول حرف ظهر مرة واحدة بس
            for (int i = 0; i < s.Length; i++)
            {
                if (counts[s[i]] == 1)
                    return i;
            }

            return -1;
        }

        static void Main(string[] args)
        {
            // ---------- Q1 ----------
            Console.WriteLine("----- Q1: Optimized Bubble Sort -----");
            int[] arr = { 64, 34, 25, 12, 22, 11, 90 };
            OptimizedBubbleSort(arr);
            Console.WriteLine(string.Join(", ", arr));

            // ---------- Q2 ----------
            Console.WriteLine("\n----- Q2: Range<T> -----");
            Range<int> intRange = new Range<int>(10, 50);
            Console.WriteLine("IsInRange(25): " + intRange.IsInRange(25));   // True
            Console.WriteLine("IsInRange(60): " + intRange.IsInRange(60));   // False
            Console.WriteLine("Length: " + intRange.Length());               // 40

            Range<double> doubleRange = new Range<double>(1.5, 4.0);
            Console.WriteLine("IsInRange(2.2): " + doubleRange.IsInRange(2.2)); // True
            Console.WriteLine("Length: " + doubleRange.Length());               // 2.5

            // ---------- Q3 ----------
            Console.WriteLine("\n----- Q3: Reverse ArrayList -----");
            ArrayList list = new ArrayList { 1, 2, 3, 4, 5 };
            Console.WriteLine("Before: " + string.Join(", ", list.ToArray()));
            ReverseArrayList(list);
            Console.WriteLine("After : " + string.Join(", ", list.ToArray()));

            // ---------- Q4 ----------
            Console.WriteLine("\n----- Q4: Even numbers -----");
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, -4 };
            List<int> evens = GetEvenNumbers(numbers);
            Console.WriteLine(string.Join(", ", evens));   // 2, 4, 6, 8, -4

            // ---------- Q5 ----------
            Console.WriteLine("\n----- Q5: FixedSizeList<T> -----");
            FixedSizeList<string> fixedList = new FixedSizeList<string>(2);
            try
            {
                fixedList.Add("A");
                fixedList.Add("B");
                Console.WriteLine("Get(1): " + fixedList.Get(1));   // B
                fixedList.Add("C");                                  // هنا Exception: القايمة مليانة
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                fixedList.Get(5);                                    // index غلط
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            // ---------- Q6 ----------
            Console.WriteLine("\n----- Q6: First non-repeated character -----");
            Console.WriteLine(FirstNonRepeatedChar("leetcode"));       // 0
            Console.WriteLine(FirstNonRepeatedChar("loveleetcode"));   // 2
            Console.WriteLine(FirstNonRepeatedChar("aabb"));           // -1
        }
    }
}