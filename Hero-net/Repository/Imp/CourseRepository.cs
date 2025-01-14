﻿namespace Hero_net.Repository;

public class CourseRepository : ICourseRepository
{
    private List<Student> _enrolledStudents;

    public CourseRepository()
    {
        _enrolledStudents = new List<Student>();
    }

    public bool Enroll(Student student)
    {
        try
        {
            _enrolledStudents.Add(student);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public Student GetStudent(Student student) 
    {
        return _enrolledStudents.FirstOrDefault(s => s.FirstName == student.FirstName && s.LastName == student.LastName);
    }
}