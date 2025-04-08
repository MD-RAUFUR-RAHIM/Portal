using Portal.Domain;
using Portal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IStudentRepository StudentRepository { get; private set; }

        public ApplicationUnitOfWork(ApplicationDbContext context, IStudentRepository studentRepository) : base(context)
        {
            StudentRepository = studentRepository;
           
        }

    }
}
