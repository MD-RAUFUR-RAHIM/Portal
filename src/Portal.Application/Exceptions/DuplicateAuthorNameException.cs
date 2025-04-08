using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Exceptions
{
    public class DuplicateAuthorNameException : Exception
    {
        public DuplicateAuthorNameException():base("Student's Name can't be duplicate")
        { 
        
        }
        public DuplicateAuthorNameException(string message) :base(message)
        {
        
        }
    }
}
