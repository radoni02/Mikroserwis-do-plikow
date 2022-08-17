using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class EmptyInputException : ProjectException
    {
        public EmptyInputException() : base($"Object not found.", 404) { }
    }
    public class BadInputException : ProjectException
    {
        public BadInputException(Guid Id) : base($"File with {Id} id not exist.", 400) { }
    }
}
