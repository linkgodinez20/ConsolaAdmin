using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Core.Model;

namespace Security.Services
{
    public interface IUserService
    {
        bool IsUnique(string login);
        void ChangePassword(int id, string password);
        Cuentas Get(string Login, string password);
    }
}
