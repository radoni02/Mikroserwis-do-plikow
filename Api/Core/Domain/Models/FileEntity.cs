using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; init; }
        public DateTime? UpdateTime { get; set; }
        public string Type { get; set; }
        public FileEntity( string name,string type)
        {
            Time = DateTime.Now;
            Name = name;
            Type = type;
        }
    }

}
