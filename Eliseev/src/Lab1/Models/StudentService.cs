using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace Lab1.Models
{
    public class StudentService
    {
        protected string pathToJsonFile;

        public StudentService(string pathToJsonFile)
        {
            this.pathToJsonFile = pathToJsonFile;
        }

        public void WriteStudentsToFile(List<StudentInfo> students)
        {
            if (!File.Exists(pathToJsonFile))
            {
                throw new Exception("Error has occured. Json file has not been found.");
            }
            File.WriteAllText(pathToJsonFile,JsonConvert.SerializeObject(students));
        }

        public List<StudentInfo> ReadStudentFromJson()
        {
            string students = File.ReadAllText(pathToJsonFile);
            List<StudentInfo> studentsInfo = JsonConvert.DeserializeObject<List<StudentInfo>>(students);
            return studentsInfo;
        }
    }
}