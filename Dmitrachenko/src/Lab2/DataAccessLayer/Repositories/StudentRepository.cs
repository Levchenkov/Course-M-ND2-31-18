using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly StudentContext dbContext;
        public StudentRepository(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Student item)
        {
            dbContext.Students.Add(item);
        }

        public void Delete(int id)
        {
            var deleteStudent = dbContext.Students.Find(id);
            if (deleteStudent != null) dbContext.Students.Remove(deleteStudent);
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return dbContext.Students.Where(predicate);
        }

        public Student Get(int id)
        {
            return dbContext.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return dbContext.Students;
        }

        public void Update(Student item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
