using Lab3mvc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3mvc.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository<string> studentRepository;
        public StudentService(IStudentRepository<string> studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public string MakeSmthWithRepository()
        {
            return "make";
        }

        public string Add()
        {
            return studentRepository.Add("");
        }
    }
}