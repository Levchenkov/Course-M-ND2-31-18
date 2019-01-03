using System.Collections.Generic;

namespace Lab1.Models
{
    public interface IStudentRepository
    {
        void Create(StudentInfo student);
        void Delete(int id);
        void Update(StudentInfo student);
        StudentInfo GetStudentById(int id);
        List<StudentInfo> GetStudents();
    }
}