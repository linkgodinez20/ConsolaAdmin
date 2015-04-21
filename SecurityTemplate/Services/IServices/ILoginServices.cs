using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Core.Model;
using Security.Core.ViewModels;

namespace Security.Services.IServices
{
    public interface ILoginServices
    {
        bool log_in(string usuario, string password, ref string msj);
        bool log_out(Int32 Cuenta_id);
        Sesiones addSession(Int32 Cuenta_id);
        bool closeSession(Int32 Cuenta_id);
        ProfileViewModel getProfile(Int32 Cuenta_id);
    }
}
