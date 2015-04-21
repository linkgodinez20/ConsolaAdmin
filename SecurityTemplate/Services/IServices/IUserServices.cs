using Security.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services.IServices
{
    public interface IUserServices
    {
        Cuentas AddAccount(Cuentas cuenta);
        bool EditAccount(Int32 id);
        bool ChangePassword(); //Revisar parámetros
        bool IsUnique();
        bool UpdateAccount(Cuentas cuenta);
        bool DeleteAccount(Int32 id);
        bool PasswordRecovery(string usuario, byte Id_Pregunta);
        Cuentas GetAccount(int id);
        IQueryable<Cuentas> GetListOfAccounts();
    }
}
