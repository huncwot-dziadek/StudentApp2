using StudentApp;
using static StudentApp.Student;

List<Student> allStudentsFromFile = new List<Student>();
List<Student> studentsWithMaximumGrade = new List<Student>();
List<Student> studentsWithMaximumAverage = new List<Student>();

string fileIn = "AllStudents.txt";
int lineCount = File.ReadAllLines(fileIn).Length;

string fileOut = "ResultsAllStudents.txt";
File.Create(fileOut).Close();

int numberOfTeaching = Enum.GetValues(typeof(subjectOfTeaching)).Length;

Console.WriteLine("Welcome to the program WHICH STUDENT WILL RECEIVE THE AWARD");
Console.WriteLine("                       ====================================");
Console.WriteLine();
Console.WriteLine("This is the first huncwot program");
Console.WriteLine("Here we go");
Console.WriteLine();

LoadingListStudentsFromFile(fileIn);

Console.WriteLine("Your grade should be in the range:");
Console.WriteLine("1, +1, 1+, -2, 2-, 2, +2, 2+, -3, 3-, 3, +3, 3+ ........ -5, 5-, 5, +5, 5+, -6, 6-, 6");
Console.WriteLine();
Console.WriteLine("Give a grades:");
Console.WriteLine();

AddingStudentGradesForAllSubjects();

WritingToFile();

FindStudentsWithTheBestStatistics();

void LoadingListStudentsFromFile(string filePath)
{
    for (int i = 0; i < lineCount; i++)
    {
        string studentNameAndSurname = File.ReadLines(filePath)
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .Skip(i)
        .FirstOrDefault();

        string[] wordsStudent = studentNameAndSurname.Split(' ');

        Student anotherStudentFromFile = new Student(wordsStudent[0], wordsStudent[1]);
        allStudentsFromFile.Add(anotherStudentFromFile);
    }

    Student anotherStudent = allStudentsFromFile[0];
}

void AddingStudentGradesForAllSubjects()
{
    Statistics statistics = new Statistics();

    foreach (var anotherStudent in allStudentsFromFile)
    {
        int numberOfSubjects = 0;

        Console.WriteLine("======================");
        Console.WriteLine($"{anotherStudent.Name} {anotherStudent.Surname}:");
        Console.WriteLine();

        for (int i = 0; i < numberOfTeaching; i++)
        {
            Console.Write($"{(Student.subjectOfTeaching)numberOfSubjects} ");

            var grade = Console.ReadLine();

            try
            {
                anotherStudent.AddGrade(grade);
                numberOfSubjects++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception catched: {ex.Message}");
                i--;
            }
        }
        Console.WriteLine("======================");
        Console.WriteLine();
    }
}

void FindStudentsWithTheBestStatistics()
{
    var gradeMax = float.MinValue;
    var averageMax = float.MinValue;

    foreach (var student in allStudentsFromFile)
    {
        gradeMax = Math.Max(gradeMax, student.GetStatistics().Max);
        averageMax = Math.Max(averageMax, student.GetStatistics().Average);
    }

    foreach (var student in allStudentsFromFile)
    {
        if (student.GetStatistics().Max == gradeMax)
        {
            studentsWithMaximumGrade.Add(student);
        }
    }

    foreach (var student in allStudentsFromFile)
    {
        if (student.GetStatistics().Average == averageMax)
        {
            studentsWithMaximumAverage.Add(student);
        }
    }
    Console.WriteLine();
    Console.WriteLine("They receive the reward:");
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine($"Student/students with the highest grade: {gradeMax}");

    foreach (var student in studentsWithMaximumGrade)
    {
        Console.WriteLine($"{student.Surname} {student.Name}");
    }
    Console.WriteLine();
    Console.WriteLine($"Student/students with the highest grade point average: {averageMax}");

    foreach (var student in studentsWithMaximumAverage)
    {
        Console.WriteLine($"{student.Surname} {student.Name}");
    }
    Console.WriteLine("------------------------------------------------------------");
}

void WritingToFile()
{
    foreach (var student in allStudentsFromFile)
    {
        using (var writer = File.AppendText(fileOut))
        {
            writer.WriteLine($"{student.Surname} {student.Name} {student.GetStatistics().Max} {student.GetStatistics().Average}");
        }
    }
}