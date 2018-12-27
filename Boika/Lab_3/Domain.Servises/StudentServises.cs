using Data.Contract;
using Data.Contract.Entities;
using Data.Implementation;
using Servises.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servises
{
    public class StudentServises : IStudentServises
    {
        private IDbContext db;
        public StudentServises(IDbContext db)
        {
            this.db = db;
        }

        public void Create(Student student)
        {
            db.Create(student);
        }


        public Student[] Read()
        {
            return db.Read();

        }

        public void Update(Student student)
        {
            db.Update(student);
        }

        public void Delete(int id)
        {
            db.Delete(id);
        }

    }
}
