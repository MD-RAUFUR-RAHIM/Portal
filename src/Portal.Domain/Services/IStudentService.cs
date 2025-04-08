using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Services
{
    public interface IStudentService 
    {
        public void AddStudent(Student student);
        void DeleteStudent(Guid id);
        (IList<Student> data, int total, int totalDisplay) GetStudents(int pageIndex, int pageSize,
            string? order, DataTablesSearch search);
        object GetStudent(Guid id);
        void Update(Student student);

    }
}
