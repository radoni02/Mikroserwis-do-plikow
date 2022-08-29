using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class UnableToDownladException : ProjectException
    {
        public UnableToDownladException(Guid id) : base($"Unable to download file with id:{id}.")
        {
        }
    }
}
