using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class UnableToUplaodFileException : ProjectException
    {
        public UnableToUplaodFileException(Guid id) : base($"Unable to upload file with id:{id}.")
        {
        }
    }
}
