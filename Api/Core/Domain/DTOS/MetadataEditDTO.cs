using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTOS
{
    public class MetadataEditDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public DateTime UpdateTime {get;set;}
        public MetadataEditDTO(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
    
}
