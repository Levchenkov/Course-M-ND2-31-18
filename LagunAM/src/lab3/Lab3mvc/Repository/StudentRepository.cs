using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3mvc.Repository
{
    public class StudentRepository : IStudentRepository<string>
    {
        public string Add(string model)
        {
            return "Student add";
        }

        public string Get(string model)
        {
            return "Student get";
        }

        public string Remove(string model)
        {
            return "Student remove";
        }

        public string Update(string model)
        {
            return "Student update";
        }
    }
}