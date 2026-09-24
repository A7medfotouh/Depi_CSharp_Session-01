using System;

namespace oopproject
{
    enum ExamType
    {
        Final = 1,
        Practical = 2
    }

    // ======================= (4) =======================
    class Answer : ICloneable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText ?? "";
        }

        public Answer() : this(0, "") { }   // constructor chaining

        public override string ToString()
        {
            return $"{AnswerId}. {AnswerText}";
        }

        public object Clone()
        {
            return new Answer(AnswerId, AnswerText);
        }
    }

    abstract class Question : ICloneable, IComparable
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] AnswerList { get; set; }   // (5) مصفوفة الإجابات
        public int RightAnswerId { get; set; }     // (5) الإجابة الصحيحة

        public abstract string QuestionType { get; }

        protected Question(string header, string body, int mark, Answer[] answerList, int rightAnswerId)
        {
            Header = header ?? "";
            Body = body ?? "";
            Mark = mark < 0 ? 0 : mark;
            AnswerList = answerList ?? new Answer[0];
            RightAnswerId = rightAnswerId;
        }

        // constructor chaining
        protected Question(string header, string body, int mark)
            : this(header, body, mark, new Answer[0], 0) { }

        public bool HasAnswer(int answerId)
        {
            foreach (Answer a in AnswerList)
            {
                if (a.AnswerId == answerId) return true;
            }
            return false;
        }

        public string GetAnswerText(int answerId)
        {
            foreach (Answer a in AnswerList)
            {
                if (a.AnswerId == answerId) return a.AnswerText;
            }
            return "(no answer)";
        }

        public void Display(int number)
        {
            Console.WriteLine($"\nQ{number}) [{QuestionType}] {Header} (Mark: {Mark})");
            Console.WriteLine($"    {Body}");
            foreach (Answer a in AnswerList)
            {
                Console.WriteLine($"      {a}");
            }
        }

        public override string ToString()
        {
            return $"{Header}: {Body} (Mark = {Mark})";
        }

        // Clone عميق: بننسخ الإجابات كمان
        public object Clone()
        {
            Question copy = (Question)MemberwiseClone();
            copy.AnswerList = new Answer[AnswerList.Length];
            for (int i = 0; i < AnswerList.Length; i++)
            {
                copy.AnswerList[i] = (Answer)AnswerList[i].Clone();
            }
            return copy;
        }

        public int CompareTo(object obj)
        {
            if (obj is not Question other) return 1;
            return Mark.CompareTo(other.Mark);
        }
    }

    // ======================= (3) =======================
    class TrueFalseQuestion : Question
    {
        public override string QuestionType => "True/False";

        public TrueFalseQuestion(string header, string body, int mark, bool correctAnswer)
            : base(header, body, mark,
                   new Answer[] { new Answer(1, "True"), new Answer(2, "False") },
                   correctAnswer ? 1 : 2)
        {
        }
    }

    class MCQQuestion : Question
    {
        public override string QuestionType => "MCQ";

        public MCQQuestion(string header, string body, int mark, string[] choices, int rightChoiceNumber)
            : base(header, body, mark, BuildAnswers(choices), rightChoiceNumber)
        {
        }

        private static Answer[] BuildAnswers(string[] choices)
        {
            if (choices == null) return new Answer[0];

            Answer[] answers = new Answer[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                answers[i] = new Answer(i + 1, choices[i]);
            }
            return answers;
        }
    }

    // ======================= (6)  =======================
    abstract class Exam : IComparable
    {
        public int Time { get; set; }                       // الوقت بالدقايق
        public Question[] Questions { get; }
        public int NumberOfQuestions => Questions.Length;   // عدد الأسئلة
        public Subject Subject { get; set; }                // (7) كل امتحان مرتبط بمادة

        protected Exam(int time, Question[] questions)
        {
            Time = time > 0 ? time : 30;
            Questions = questions ?? new Question[0];
        }

        protected Exam(Question[] questions) : this(30, questions) { }

        public abstract void ShowExam();
        protected static int ReadAnswerId(Question question)
        {
            int id;
            Console.Write("Your answer (enter the answer number): ");
            while (!int.TryParse(Console.ReadLine(), out id) || !question.HasAnswer(id))
            {
                Console.Write("Invalid choice, try again: ");
            }
            return id;
        }

        public override string ToString()
        {
            return $"{GetType().Name} | Subject: {Subject?.SubjectName} | Time: {Time} min | Questions: {NumberOfQuestions}";
        }

        public int CompareTo(object obj)
        {
            if (obj is not Exam other) return 1;
            return Time.CompareTo(other.Time);
        }
    }

    class FinalExam : Exam
    {
        public FinalExam(int time, Question[] questions) : base(time, questions) { }
        public FinalExam(Question[] questions) : base(questions) { }

        public override void ShowExam()
        {
            Console.WriteLine($"===== Final Exam - {Subject?.SubjectName} | Time: {Time} min =====");

            int[] studentAnswers = new int[Questions.Length];
            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i].Display(i + 1);
                studentAnswers[i] = ReadAnswerId(Questions[i]);
            }

            Console.WriteLine("\n===== Exam Result =====");
            int grade = 0;
            int total = 0;
            for (int i = 0; i < Questions.Length; i++)
            {
                Question q = Questions[i];
                total += q.Mark;
                if (studentAnswers[i] == q.RightAnswerId)
                    grade += q.Mark;

                Console.WriteLine($"Q{i + 1}) {q.Body}");
                Console.WriteLine($"    Your answer: {q.GetAnswerText(studentAnswers[i])}");
            }
            Console.WriteLine($"\nGrade: {grade} / {total}");
        }
    }

    // (8) Practical: بيعرض الإجابة الصحيحة بعد ما الطالب يخلص
    class PracticalExam : Exam
    {
        public PracticalExam(int time, MCQQuestion[] questions) : base(time, questions) { }
        public PracticalExam(MCQQuestion[] questions) : base(questions) { }

        public override void ShowExam()
        {
            Console.WriteLine($"===== Practical Exam - {Subject?.SubjectName} | Time: {Time} min =====");

            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i].Display(i + 1);
                ReadAnswerId(Questions[i]);
            }

            Console.WriteLine("\n===== Right Answers =====");
            for (int i = 0; i < Questions.Length; i++)
            {
                Question q = Questions[i];
                Console.WriteLine($"Q{i + 1}) {q.Body}");
                Console.WriteLine($"    Right answer: {q.GetAnswerText(q.RightAnswerId)}");
            }
        }
    }

    // ======================= (7)  =======================
    class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; private set; }   // امتحان المادة

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName ?? "Unknown";
        }

        public Subject() : this(0, "Unknown") { }   // constructor chaining

        public Exam CreateExam(ExamType type)
        {
            switch (type)
            {
                case ExamType.Final:
                    Exam = new FinalExam(30, new Question[]
                    {
                        new TrueFalseQuestion("Basics", "C# is a case-sensitive language.", 5, true),
                        new TrueFalseQuestion("OOP", "An abstract class can be instantiated.", 5, false),
                        new MCQQuestion("Inheritance", "Which symbol is used for inheritance in C#?", 10,
                                        new[] { "->", "::", ":", "=>" }, 3),
                        new MCQQuestion("Keywords", "Which keyword prevents a class from being inherited?", 10,
                                        new[] { "abstract", "sealed", "virtual", "partial" }, 2)
                    });
                    break;

                default:
                    Exam = new PracticalExam(45, new MCQQuestion[]
                    {
                        new MCQQuestion("Entry Point", "Which method is the entry point of a console app?", 10,
                                        new[] { "Main", "Start", "Run", "Init" }, 1),
                        new MCQQuestion("Polymorphism", "Which keyword allows a method to be overridden?", 10,
                                        new[] { "override", "virtual", "new", "sealed" }, 2),
                        new MCQQuestion("Interfaces", "Which interface is used to clone objects?", 10,
                                        new[] { "IComparable", "IDisposable", "ICloneable", "IEnumerable" }, 3)
                    });
                    break;
            }

            Exam.Subject = this;
            return Exam;
        }
    }

    class Program
    {
        static int ReadExamChoice()
        {
            int choice;
            Console.Write("Choose exam type (1 = Final, 2 = Practical): ");
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.Write("Invalid choice, enter 1 or 2: ");
            }
            return choice;
        }

        static void Main(string[] args)
        {
            Subject subject = new Subject(1, "C# Programming");

            int choice = ReadExamChoice();
            subject.CreateExam((ExamType)choice);

            Console.WriteLine(subject.Exam);
            Console.WriteLine();
            subject.Exam.ShowExam();

            Console.WriteLine("\n===== Clone & Compare demo =====");
            Question first = subject.Exam.Questions[0];
            Question second = subject.Exam.Questions[subject.Exam.Questions.Length - 1];

            Question copy = (Question)first.Clone();
            Console.WriteLine("Clone is a different object: " + !ReferenceEquals(first, copy));
            Console.WriteLine("Clone has same data        : " + (first.ToString() == copy.ToString()));
            Console.WriteLine("first.CompareTo(last) (by Mark): " + first.CompareTo(second));
        }
    }
}