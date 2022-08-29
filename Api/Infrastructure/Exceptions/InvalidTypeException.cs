using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class InvalidTypeException : ProjectException
    {
        public InvalidTypeException(string type) : base($"Invalid Content type. {type} is not accesible.")
        {
        }
    }
}
