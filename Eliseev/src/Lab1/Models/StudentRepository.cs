using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace Lab1.Models
{
    public class StudentRepository : IStudentRepository
    {
        protected string pathToJsonFile = HostingEnvironment.MapPath(@"~/App_Data/students.json");
        
        private StudentService studentService;
        protected List<StudentInfo> studentList;

        public StudentRepository()
        {
            studentService = new StudentService(pathToJsonFile);
            studentList = studentService.ReadStudentFromJson();
        }

        public void Create(StudentInfo student)
        {
            if (studentList == null)
            {
                studentList = new List<StudentInfo>();
                student.Id = 1;
            }
            else
            {
                student.Id = studentList[studentList.Count-1].Id + 1;
            }
            studentList.Add(student);
            studentService.WriteStudentsToFile(studentList);
        }

        public void Update(StudentInfo student)
        {
            foreach(StudentInfo studentInfo in studentList)
            {
                if (studentInfo.Id == student.Id)
                {
                    studentInfo.FirstName = student.FirstName;
                    studentInfo.SecondName = student.SecondName;
                    studentInfo.Age = student.Age;
                    break;
                }
            }
            studentService.WriteStudentsToFile(studentList);
        }

        public void Delete(int id)
        {
            try
            {
                studentList.RemoveAll(student => student.Id == id);
            }
            catch
            {
                throw new Exception("You are trying to delete a not existing student");
            }
            
            if (studentList.Count == 0)
            {
                studentList = null;
                new FileInfo(pathToJsonFile).Open(FileMode.Truncate).Close();
            }
            else
            {
                studentService.WriteStudentsToFile(studentList);
            }
        }

        public StudentInfo GetStudentById(int id)
        {
            try
            {
                return studentList.Find(student => student.Id == id);
            }
            catch
            {
                throw new Exception($"There are no a student with id={id} in the repository");
            }
        }

        public List<StudentInfo> GetStudents()
        {
            return studentList;
        }
    }
}