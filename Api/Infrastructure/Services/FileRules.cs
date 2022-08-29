using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FileRules
    {
        private HashSet<string> _validTypes;

        public FileRules()
        {
            _validTypes = new HashSet<string>()
            {
                ".txt", ".rtf", ".doc", ".docx", ".pdf",
                ".jpg", ".jpeg", ".bmp", ".png",
                ".svg",
                ".mp4", ".rmvb", ".mov",
                ".mp3"
            };

        }
        public string NameTolower(string name)
        {
            return name.ToLower();
        }
        public bool Isvalid(string type)
        {
            foreach(var t in _validTypes)
            {
                if(type==t)
                {
                    return true;
                }
            }
            return false;  
        }

        public string ChangeFileName(string oldName)
        {
            var ext = Path.GetExtension(oldName);
            return ext;
        }
    }
}
