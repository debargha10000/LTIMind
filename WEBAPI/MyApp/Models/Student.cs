using System;

namespace MyApp.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }

    public List<StudentCourse> StudentCourses { get; set; }
}
