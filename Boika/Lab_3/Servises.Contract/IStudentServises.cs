using Data.Contract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Contract
{
    public interface IStudentServises
    {
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
        Student[] Read();
    }
}
