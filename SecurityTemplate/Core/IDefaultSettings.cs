using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Core.DefaultSettings
{
    public interface IDefaultSettings
    {
        String Sistema_Nombre { get; }

        Byte Sistema_iD { get; }

        String Sistema_HashKey { get; }

        String Sistema_ConnectionString { get; }

        String Directorio_PersonasFoto { get; }

        String Directorio_Imagenes { get; }

        String Directorio_Iconos { get; }

    }
}
