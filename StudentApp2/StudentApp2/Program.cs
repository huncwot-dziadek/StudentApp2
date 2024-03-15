using StudentApp;
using static StudentApp.Student;

List<Student> studentsWithMaximumGrade = new List<Student>();
List<Student> studentsWithMaximumAverage = new List<Student>();

string fileOut = "ResultsAllStudents.txt";
File.Create(fileOut).Close();

int numberOfTeaching = Enum.GetValues(typeof(subjectOfTeaching)).Length;

Console.WriteLine("Welcome to the program WHICH STUDENT WILL RECEIVE THE AWARD");
Console.WriteLine("                       ====================================");
Console.WriteLine();
Console.WriteLine("This is the first huncwot program");
Console.WriteLine("Here we go");
Console.WriteLine();

Student student1 = new Student("Maria", "Kowalska");
Student.allStudentsFromFile.Add(student1);
Student student2 = new Student("Zenon", "Malinka");
Student.allStudentsFromFile.Add(student2);
Student student3 = new Student("Hanna", "Wanna");
Student.allStudentsFromFile.Add(student3);
//Student student4 = new Student("Jan", "Dzban");
//Student.allStudentsFromFile.Add(student4);

Console.WriteLine("Your grade should be in the range:");
Console.WriteLine("1, +1, 1+, -2, 2-, 2, +2, 2+, -3, 3-, 3, +3, 3+ ........ -5, 5-, 5, +5, 5+, -6, 6-, 6");
Console.WriteLine();
Console.WriteLine("Give a grades:");
Console.WriteLine();

AddingStudentsGradesAndWritingToFile();

WritingToFile();

FindStudentsWithTheBestStatistics();

void AddingStudentsGradesAndWritingToFile()
{
    Statistics statistics = new Statistics();

    foreach (var anotherStudent in allStudentsFromFile)
    {
        Student.fileName = $"{anotherStudent.Name} {anotherStudent.Surname}.txt";

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





//string fileIn = "AllStudents.txt";
//int lineCount = File.ReadAllLines(fileIn).Length;

//LoadingListStudentsFromFile(fileIn);

//void LoadingListStudentsFromFile(string filePath)
//{
//    for (int i = 0; i < lineCount; i++)
//    {
//        string studentNameAndSurname = File.ReadLines(filePath)
//        .Where(line => !string.IsNullOrWhiteSpace(line))
//        .Skip(i)
//        .FirstOrDefault();

//        string[] wordsStudent = studentNameAndSurname.Split(' ');

//        Student anotherStudentFromFile = new Student(wordsStudent[0], wordsStudent[1]);
//        allStudentsFromFile.Add(anotherStudentFromFile);
//    }

//    Student anotherStudent = allStudentsFromFile[0];
//}
