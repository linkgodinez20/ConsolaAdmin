using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Security.Core.Model
{
    public class FileModel
    {
        public object file { get; set; }
        public string filename { get; set; }
        public string extension { get; set; }
        public string fullpath { get; set; }
    }
}
