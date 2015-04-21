using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Security.Services.IServices;
using Security.Core.Model;
using Security.Core.Repository;
using System.Transactions;
using Security.Core.Settings;

namespace Security.Services.Services
{
    public class UserService: IUserServices, IDisposable
    {

        public Cuentas AddAccount(Cuentas cuenta)
        {
            throw new NotImplementedException();
        }

        public bool EditAccount(int id)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword()
        {
            throw new NotImplementedException();
        }

        public bool IsUnique()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAccount(Cuentas cuenta)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public bool PasswordRecovery(string usuario, byte Id_Pregunta)
        {
            throw new NotImplementedException();
        }

        public Cuentas GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cuentas> GetListOfAccounts()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
