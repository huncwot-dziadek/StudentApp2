namespace StudentApp;
public class Statistics
{
    public float Max { get; private set; }

    public float Sum { get; private set; }

    public int Count { get; private set; }

    public float Average
    {
        get
        {
            return this.Sum / this.Count;
        }
    }

    public Statistics()
    {
        this.Count = 0;
        this.Sum = 0;
        this.Max = float.MinValue;
    }

    public void AddGrade(float grade)
    {
        this.Count++;
        this.Sum += grade;
        this.Max = Math.Max(this.Max, grade);
    }
}
