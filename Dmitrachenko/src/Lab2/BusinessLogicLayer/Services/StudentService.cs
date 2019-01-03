using AutoMapper;
using BusinessLogicLayer.DataModel;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }

        public StudentService(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            UnitOfWork = iUnitOfWork;
            Mapper = mapper;
        }

        public IEnumerable<StudentDataModel> GetAllStudents()
        {
            var studentDataModel = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDataModel>>(UnitOfWork.Students.GetAll());
            return studentDataModel;
        }

        public int Create(StudentDataModel studentDataModel)
        {
            var newStudent = Mapper.Map<StudentDataModel, Student>(studentDataModel);
            UnitOfWork.Students.Create(newStudent);
            UnitOfWork.Save();
            return newStudent.Id;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public StudentDataModel Get(StudentDataModel studentDataModel)
        {
            var currentStudent = UnitOfWork.Students
                .Find(s => s.FirstName == studentDataModel.FirstName && s.LastName == studentDataModel.LastName)
                .FirstOrDefault();
            if (currentStudent != null)
            {
                return Get(currentStudent.Id);
            }
            return null;
        }

        public StudentDataModel Get(int id)
        {
            var student = UnitOfWork.Students.Get(id);
            var getStudentDataModel = Mapper.Map<Student, StudentDataModel>(student);
            return getStudentDataModel;
        }

        public void Edit(StudentDataModel studentDataModel)
        {
            var editStudent = Mapper.Map<StudentDataModel, Student>(studentDataModel);
            UnitOfWork.Students.Update(editStudent);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            UnitOfWork.Students.Delete(id);
            UnitOfWork.Save();
        }
    }
}
