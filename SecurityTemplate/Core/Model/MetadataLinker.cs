using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Core.Model
{
    [MetadataType(typeof(SistemasMetadata))]
    public partial class Sistemas
    {    }

    [MetadataType(typeof(AreasDeTrabajoMetadata))]
    public partial class AreasDeTrabajo
    {    }

    [MetadataType(typeof(Baja_motivosMetadata))]
    public partial class Baja_motivos
    {    }

    [MetadataType(typeof(Beneficio_tipoMetadata))]
    public partial class Beneficio_tipo 
    {    }
    
    [MetadataType(typeof(Contacto_medioMetadata))]
    public partial class Contacto_medio
    {    }

    [MetadataType(typeof(CuentasMetadata))]
    public partial class Cuentas
    {    }

    [MetadataType(typeof(DirectoriosMetadata))]
    public partial class Directorios
    {    }

    [MetadataType(typeof(EntidadesMetadata))]
    public partial class Entidades
    {    }

    [MetadataType(typeof(Menu_categoriaMetadata))]
    public partial class Menu_categoria
    {    }

    [MetadataType(typeof(LogInMetadata))]
    public partial class LogIn
    {    }

    [MetadataType(typeof(PaginasMetadata))]
    public partial class Paginas
    {    }

    [MetadataType(typeof(PermisosMetadata))]
    public partial class Permisos
    {    }

    [MetadataType(typeof(LocationsMetadata))]
    public partial class Locations
    {    }

    [MetadataType(typeof(Equipos_tipoMetadata))]
    public partial class Equipos_tipo
    {    }

    [MetadataType(typeof(EquiposMetadata))]
    public partial class Equipos
    { }

    [MetadataType(typeof(Permisos_Cuentas_x_ActividadesMetadata))]
    public partial class Permisos_Cuentas_x_Actividades
    {    }

    [MetadataType(typeof(PreguntasMetadata))]
    public partial class Pregntas
    { }

    [MetadataType(typeof(Preguntas_x_LoginMetadata))]
    public partial class Preguntas_x_Login
    { }

    [MetadataType(typeof(EventsMetadata))]
    public partial class Events
    { }

    [MetadataType(typeof(PersonasMetadata))]
    public partial class Personas
    { }

    [MetadataType(typeof(ActividadesMetadata))]
    public partial class Actividades { }

    [MetadataType(typeof(BajaMetadata))]
    public partial class Baja { }

    [MetadataType(typeof(BeneficioMetadata))]
    public partial class Beneficio { }

    [MetadataType(typeof(ContactoMetadata))]
    public partial class Contacto { }

    [MetadataType(typeof(Contacto_tipoMetadata))]
    public partial class Contacto_tipo { }

    [MetadataType(typeof(DomiciliosMetadata))]
    public partial class Domicilios { }

    [MetadataType(typeof(GeneroMetadata))]
    public partial class Genero { }

    [MetadataType(typeof(MenuMetadata))]
    public partial class Menu { }

    [MetadataType(typeof(MunicipiosMetadata))]
    public partial class Municipios { }

    [MetadataType(typeof(PaisesMetadata))]
    public partial class Paises { }

    [MetadataType(typeof(PerfilesMetadata))]
    public partial class Perfiles { }

    [MetadataType(typeof(PreguntasMetadata))]
    public partial class Preguntas { }

    [MetadataType(typeof(SesionesMetadata))]
    public partial class Sesiones { }

}
