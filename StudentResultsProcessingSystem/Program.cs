using System.Globalization;

namespace StudentResultsProcessingSystem;

internal class Student
{
    public string FullName { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string Programme { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public double[] Scores { get; set; } = new double[Program.CourseNames.Length];

    public double Total => Scores.Sum();
    public double Average => Scores.Length == 0 ? 0 : Total / Scores.Length;
    public string Grade => Program.GetGrade(Average);
    public string AcademicStatus => Average >= 50 ? "Passed" : "Failed";
}

internal static class Program
{
    public static readonly string[] CourseNames =
    {
        "Programming with C#",
        "Database Systems",
        "Computer Networks",
        "Web Development",
        "Mathematics for Computing"
    };

    private const int RequiredStudentCount = 3;
    private static readonly List<Student> Students = new();

    private static void Main()
    {
        Console.Title = "Student Results Processing System";

        bool keepRunning = true;
        while (keepRunning)
        {
            DisplayMenu();
            string choice = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    EnterStudentResults();
                    break;
                case "2":
                    ViewStudentReport();
                    break;
                case "3":
                    Console.WriteLine("Thank you for using the Student Results Processing System.");
                    keepRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose 1, 2, or 3.\n");
                    break;
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
        Console.WriteLine();
        Console.WriteLine("1. Enter Student Results");
        Console.WriteLine("2. View Student Report");
        Console.WriteLine("3. Exit");
        Console.WriteLine();
        Console.Write("Choose an option: ");
    }

    private static void EnterStudentResults()
    {
        Students.Clear();

        for (int studentNumber = 1; studentNumber <= RequiredStudentCount; studentNumber++)
        {
            Console.WriteLine($"----- Enter details for Student {studentNumber} -----");

            Student student = new()
            {
                FullName = ReadRequiredText("Enter full name: "),
                StudentId = ReadRequiredText("Enter student ID: "),
                Programme = ReadRequiredText("Enter programme: "),
                Level = ReadRequiredText("Enter level: ")
            };

            Console.WriteLine();
            for (int courseIndex = 0; courseIndex < CourseNames.Length; courseIndex++)
            {
                student.Scores[courseIndex] = ReadValidScore(
                    $"Enter score for {CourseNames[courseIndex]}: ");
            }

            Students.Add(student);
            Console.WriteLine($"\nResults for {student.FullName} saved successfully.\n");
        }
    }

    private static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("This field is required. Please enter a value.");
        }
    }

    private static double ReadValidScore(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            bool isNumber = double.TryParse(
                input,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out double score);

            if (isNumber && score >= 0 && score <= 100)
            {
                return score;
            }

            Console.WriteLine("Invalid score. Score must be between 0 and 100.");
        }
    }

    private static void ViewStudentReport()
    {
        if (Students.Count == 0)
        {
            Console.WriteLine("No student results are available. Please choose option 1 first.\n");
            return;
        }

        Console.WriteLine("=============== STUDENT RESULTS REPORT ===============");

        foreach (Student student in Students)
        {
            DisplaySingleStudentReport(student);
        }

        DisplayClassSummary();
    }

    private static void DisplaySingleStudentReport(Student student)
    {
        Console.WriteLine();
        Console.WriteLine($"Student Name : {student.FullName}");
        Console.WriteLine($"Student ID   : {student.StudentId}");
        Console.WriteLine($"Programme    : {student.Programme}");
        Console.WriteLine($"Level        : {student.Level}");
        Console.WriteLine(new string('-', 56));
        Console.WriteLine($"{"Course",-38}{"Score",10}");
        Console.WriteLine(new string('-', 56));

        for (int courseIndex = 0; courseIndex < CourseNames.Length; courseIndex++)
        {
            Console.WriteLine($"{CourseNames[courseIndex],-38}{student.Scores[courseIndex],10:0.##}");
        }

        Console.WriteLine(new string('-', 56));
        Console.WriteLine($"Total Score  : {student.Total:0.##}");
        Console.WriteLine($"Average Score: {student.Average:0.0}");
        Console.WriteLine($"Grade        : {student.Grade}");
        Console.WriteLine($"Status       : {student.AcademicStatus}");
        Console.WriteLine(new string('=', 56));
    }

    private static void DisplayClassSummary()
    {
        Student bestStudent = Students.MaxBy(student => student.Average)!;
        Student lowestStudent = Students.MinBy(student => student.Average)!;
        double classAverage = Students.Average(student => student.Average);

        Console.WriteLine();
        Console.WriteLine("==================== CLASS SUMMARY ====================");
        Console.WriteLine($"Best student          : {bestStudent.FullName} ({bestStudent.Average:0.0})");
        Console.WriteLine($"Lowest average        : {lowestStudent.FullName} ({lowestStudent.Average:0.0})");
        Console.WriteLine($"Class average         : {classAverage:0.0}");
        Console.WriteLine(new string('=', 56));
        Console.WriteLine();
    }

    public static string GetGrade(double average)
    {
        if (average >= 80) return "A";
        if (average >= 70) return "B";
        if (average >= 60) return "C";
        if (average >= 50) return "D";
        return "F";
    }
}
