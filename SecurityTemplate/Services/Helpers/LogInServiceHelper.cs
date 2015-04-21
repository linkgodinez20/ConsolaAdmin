using Security.Core.Settings;
using Security.Core.Model;
using Security.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services.Helpers
{
    public class LogInServiceHelper
    {
        private readonly IRepo<Cuentas> repo_cuentas;
        private readonly IDefaultSettings settings;

        public LogInServiceHelper(IRepo<Cuentas> repo_cuentas, IDefaultSettings settings)
        {
            this.repo_cuentas = repo_cuentas;
            this.settings = settings;
        }
        /// <summary>
        /// Función que regresa el valor del salt para el usaurio espeficado
        /// </summary>
        /// <param name="usuario">test</param>
        /// <returns><b>String</b>: Salt</returns>
        public string getSalt(string usuario)
        {

            string _sal = string.Empty;

            var sal = (from nfo in repo_cuentas.GetAll()
                       where nfo.Usuario == usuario && nfo.Id_Sistema == settings.Sistema_ID
                       select nfo.Salt).FirstOrDefault();

            foreach (var slt in sal)
            {
                _sal = slt.ToString();
            }

            return _sal;
        }

        public int getCuenta_Id(string usuario)
        {
            int idCuenta = 0;

            var values = from c in repo_cuentas.GetAll()
                        where c.Usuario == usuario && c.Id_Sistema == settings.Sistema_ID
                        select c.Id_Cuenta;

            foreach(var value in values)
            {
                idCuenta = value;
            }

            return idCuenta;
        }
    }
}
