using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MinIO
{
    public class MinIOParameters
    {
        public string Endpoint{ get; set; }
        public string AccessKey{ get; set; }
        public string SecretKey{ get; set; }
        public bool Secure{ get; set; }
    }
}
