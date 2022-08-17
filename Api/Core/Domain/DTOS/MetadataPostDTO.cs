using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTOS
{
    public class MetadataPostDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public MetadataPostDTO(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
