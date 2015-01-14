using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Security
{
    /// <summary>
    /// Configuración de inicio de la aplicación
    /// </summary>
    public static class DefaultSettings
    {
        static Byte idSistema = 1;
        static string nombreSistema = "Sistema de prueba"; //opcional
        static string cipherKey = "LOZL4kVrTLqBU8bBjFD/beWvA0IY2sju";
        static String cnnString = "SecurityEntities";


        public static string NombreSistema
        {
            get
            {
                return nombreSistema;
            }

        }

        public static Byte Sistema_iD
        {
            get
            {
                return idSistema;
            }

        }

        public static string Hky
        {
            get
            {
                return cipherKey;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return cnnString;
            }
        }

    }
}