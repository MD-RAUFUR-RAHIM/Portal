using Microsoft.EntityFrameworkCore;
using Portal.Domain;
using Portal.Domain.Entities;
using Portal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student, Guid>, IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentRepository(ApplicationDbContext context)
           : base(context)
        {
            _dbContext = context;
        }

        public bool IsNameDuplicate(string name, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => x.Id != id.Value && x.Name == name) > 0;
            }
            else
            {
                return GetCount(x => x.Name == name) > 0;
            }
        }

        public (IList<Student> data, int total, int totalDisplay) GetPagedStudents(int pageIndex,
            int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(null, order, null, pageIndex, pageSize, true);

            }
            else
                return GetDynamic(x => x.Name.Contains(search.Value), order,
                    null, pageIndex, pageSize, true);
        }
    }
}
