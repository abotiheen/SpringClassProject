using System.Collections.Generic;

public static class StudentManager
{
    public static List<Student> Students { get; private set; } = new List<Student>
    {
        new Student("Muqtada Majeed", 18, "Computer Science"),
        new Student("Zain Abotiheen", 18, "Computer Science"),
        new Student("Kadhim Riyadh", 18, "History"),
        new Student("Hussein Fouad", 21, "Physics"),
        new Student("Ameer Ghassan", 19, "Archeology"),
        new Student("Jawad Ali", 17, "Physics"),
        new Student("Sajjad Amir", 20, "Psychology"),
        new Student("Yousif Oday", 20, "Psychology")
    };

    public static void AddStudent(Student student)
    {
        Students.Add(student);
    }
}

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Class { get; set; }

    public Student(string name, int age, string className)
    {
        Name = name;
        Age = age;
        Class = className;
    }
}
