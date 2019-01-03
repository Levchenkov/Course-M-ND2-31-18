using BusinessLogicLayer.DataModel;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentDataModel> GetAllStudents();
        StudentDataModel Get(StudentDataModel studentDataModel);
        int Create(StudentDataModel studentDataModel);
        StudentDataModel Get(int id);
        void Edit(StudentDataModel studentDataModel);
        void Delete(int id);
        void Dispose();
    }
}
