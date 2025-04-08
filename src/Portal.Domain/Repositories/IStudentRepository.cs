using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Repositories
{
    public interface IStudentRepository : IRepository<Student, Guid>
    {
        public bool IsNameDuplicate(string name, Guid? id = null);
        (IList<Student> data, int total, int totalDisplay) GetPagedStudents(int pageIndex,
            int pageSize, string? order, DataTablesSearch search);
        void Update(Student student);
    }
}
