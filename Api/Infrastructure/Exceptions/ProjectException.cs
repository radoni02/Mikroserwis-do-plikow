using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ProjectException : Exception
    {
        public int _errorCode { get; }
        protected ProjectException(string message, int errorCode = 400) : base(message)
        {
            _errorCode = errorCode;
        }
    }
}
