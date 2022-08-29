using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class UnableToDeleteException : ProjectException
    {
        public UnableToDeleteException(Guid id) : base($"Unable to delete file with id:{id}.",404)
        {
        }
    }
}
