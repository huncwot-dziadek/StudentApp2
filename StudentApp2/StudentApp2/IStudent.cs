namespace StudentApp
{
    public interface IStudent
    {
        string Name { get; }

        string Surname { get; }

        void AddGrade(float grade);

        void AddGrade(string grade);

        Statistics GetStatistics();
    }
}