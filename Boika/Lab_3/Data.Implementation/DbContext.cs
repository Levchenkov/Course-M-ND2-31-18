using Data.Contract;
using Data.Contract.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data.Implementation
{
    public class DbContext : IDbContext
    {
        private string path;

        public DbContext()
        {
            path = @"D:\Курс по ASP.NET MVC\HomeWorks\Lab_3\Data.Implementation\App_Data\DbStudent.json";
            if (!File.Exists(path))
            {
                using (StreamWriter streamWrtiter = new StreamWriter(File.Create(path)))
                {
                    streamWrtiter.Write("[]");
                }
            }
        }

        public void Create(Student student)
        {
            var listStudents = Read();
            if (student.Id == 0)
            {
                if (listStudents != null && listStudents.Length != 0)
                {
                    var lastStudent = listStudents.Last();
                    student.Id = (lastStudent.Id) + 1;
                }
                else
                {
                    student.Id = 1;
                }
            }

            var item = (listStudents != null) ? listStudents.ToList() : new List<Student>();
            item.Add(student);
            listStudents = item.ToArray();

            var json = JsonConvert.SerializeObject(listStudents);
            File.WriteAllText(path, json);
        }

        public Student[] Read()
        {
            var listStudents = JsonConvert.DeserializeObject<Student[]>(File.ReadAllText(path));
            return listStudents;
        }

        public void Update(Student student)
        {
            Delete(student.Id);
            Create(student);
        }

        public void Delete(int id)
        {
            var listStudents = new List<Student>();
            
            foreach(var item in Read())
            {
                if (item.Id != id)
                {
                    listStudents.Add(item);
                }
            }

            var json = JsonConvert.SerializeObject(listStudents.ToArray());
            File.WriteAllText(path, json);
        }

        
    }
}