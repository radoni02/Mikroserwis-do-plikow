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
        public DateTime? UpdateTime { get; init; }
        public string Type { get; set; }
    }
}
