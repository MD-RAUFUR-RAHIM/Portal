using Portal.Domain;
using Portal.Domain.Entities;
using Portal.Domain.Services;
using System.Data;

namespace Portal.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public StudentService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public void AddStudent(Student student)
        {
            if (!_applicationUnitOfWork.StudentRepository.IsNameDuplicate(student.Name))
            {
                _applicationUnitOfWork.StudentRepository.Add(student);
                _applicationUnitOfWork.Save();
            }
            else
            {
                throw new DuplicateNameException("Student Name can't be duplicate");
            }

        }

        public void DeleteStudent(Guid id)
        {
            _applicationUnitOfWork.StudentRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public object GetStudent(Guid id)
        {
            return _applicationUnitOfWork.StudentRepository.GetById(id);
        }

        public (IList<Student> data, int total, int totalDisplay) GetStudents(int pageIndex,
            int pageSize, string? order, DataTablesSearch search)
        {
            return _applicationUnitOfWork.StudentRepository.GetPagedStudents(pageIndex, pageSize,
                order, search);
        }

        public void Update(Student student)
        {
            _applicationUnitOfWork.StudentRepository.Update(student);
            _applicationUnitOfWork.Save();
        }
    }
}
