using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Security.Core.Model;
using Security.Core.Repository;
using Security.Core.Settings;

namespace Security
{
    /// <summary>
    /// Configuración de inicio de la aplicación
    /// </summary>
    public class DefaultSettings: IDefaultSettings
    {
        // propiedades

        private string hashKey;
        private byte sistemaId;

        public string HashKey
        {
            get
            {
                this.hashKey = "LOZL4kVrTLqBU8bBjFD/beWvA0IY2sju";
                return this.hashKey;
            }
        }

        public byte Sistema_ID
        {
            get
            {
                this.sistemaId = 1;
                return this.sistemaId;
            }
        }
    }
}