﻿namespace StudentApp
{
    public class Student : StudentBase
    {

        private List<float> grades = new List<float>();

        public static List<Student> allStudentsFromFile = new List<Student>();

        public static string fileName;

        private bool oneOfTheConditionsIsMet;

        public enum subjectOfTeaching
        {
            combinatorics______,
            algebra____________,
            physics____________,
            communicativeness__,
            teamwork_ability___
        }

        public Student(string name, string surname) : base(name, surname)
        {
        }

        public override void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 100)
            {
            using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(grade);
                }
            }
            else
            {
                throw new Exception("This grade is out of range");
            }
        }

        public override void AddGrade(string grade)
        {
            if (grade == "")
            {
                throw new Exception("You must provide a grade");
            }

            if (grade.Length == 1)
            {
                if (grade[0] >= 49 && grade[0] <= 54)
                {
                    var rating = grade[0] - 48;
                    var valueRating = (rating - 1) * 20;
                    AddGrade((float)valueRating);
                }
                else
                {
                    throw new Exception("This grade is not float or is bigger 6. Give the correct grade");
                }
            }

            if (grade.Length == 2)
            {
                oneOfTheConditionsIsMet = false;
                int j = 1;

                for (int i = 0; i < grade.Length; i++, j--)
                {
                    if ((grade[i] >= 49 && grade[i] <= 54) && (grade[j] == 43))
                    {
                        var rating = grade[i] - 48;
                        var valueRating = ((rating - 1) * 20) + 5;
                        AddGrade((float)valueRating);
                        oneOfTheConditionsIsMet = true;
                    }
                    else if ((grade[i] >= 49 && grade[i] <= 54) && (grade[j] == 45))
                    {
                        var rating = grade[i] - 48;
                        var valueRating = ((rating - 1) * 20) - 5;
                        AddGrade((float)valueRating);
                        oneOfTheConditionsIsMet = true;
                    }
                }

                if (oneOfTheConditionsIsMet == false)
                {
                    throw new Exception("Incorrectly entered grade");
                }
            }

            if (grade.Length > 2)
            {
                throw new Exception("This grade is not float or is bigger 6");
            }
        }

        public override Statistics GetStatistics()
        {
            //foreach (var student in allStudentsFromFile)
            //{

                //Student student = Student.allStudentsFromFile[0];

                //Student.fileName = $"{student.Name} {student.Surname}.txt";

                var statistics = new Statistics();

                if (File.Exists(fileName))
                {
                    using (var reader = File.OpenText(fileName))
                    {
                        var line = reader.ReadLine();

                        while (line != null)
                        {
                            //var line = reader.ReadLine();
                            if (float.TryParse(line, out float number))
                            {
                                statistics.AddGrade(number);
                            }
                            line = reader.ReadLine();
                        }
                    }
                }
                return statistics;

            //}
        }

        //public override Statistics GetStatistics()
        //{
        //    var statistics = new Statistics();

        //    foreach (var grade in grades)
        //    {
        //        statistics.AddGrade(grade);
        //    }

        //    return statistics;
        //}
    }
}
