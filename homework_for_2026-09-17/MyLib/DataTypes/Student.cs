namespace MyLib.DataTypes;

public struct Student
{
    public string FullName;
    public string GradeBookNumber;
    public Faculty Faculty;
    public int Course;
    public double AverageGrade;
    public DateTime EnrollmentDate;
}

public enum Faculty
{
    IT,
    Economics,
    Law,
    Design,
    Unknown 
}