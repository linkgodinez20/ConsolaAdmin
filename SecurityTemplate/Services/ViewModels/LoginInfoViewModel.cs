using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services.ViewModels
{
    public class LoginInfoViewModel
    {
        public string salt { get; set; }
        public int id_login { get; set; }
        public int id_cuenta { get; set; }
    }
}
