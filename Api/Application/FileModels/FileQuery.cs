using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileModels
{
    public record FileQuery(Stream Stream, string ContentType, string FileName)
        {
            public Guid MinioID { get; set; }
        }  
}
