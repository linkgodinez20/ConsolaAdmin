using System;
using System.ComponentModel.DataAnnotations;

namespace Security.Core.Model
{
    public class SistemasMetadata
    {        
        [Display(Name = "Sistema")]
        public byte Id_Sistema ;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio")]
        [StringLength(32)]
        public String Nombre;

        [StringLength(10)]
        public String Siglas;

        [StringLength(128)]
        [Display(Name = "Descripción")]
        public String Descripcion;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio")]
        public Boolean Estatus;
    }

    public class AreasDeTrabajoMetadata
    {        
        [Display(Name = "Area de trabajo")]
        public short Id_AreaDeTrabajo;

        [Required()]
        [StringLength(64)]
        [Display(Name = "Area de Trabajo")]
        public string Nombre;

        [Required()]
        [Display(Name = "Sistema")]
        public byte Id_Sistema;
    }

    public class Baja_motivosMetadata
    {
         [Display(Name = "Motivo Baja")]
        public byte Id_MotivoBaja;

        [Required()]
        [Display(Name = "Motivo Baja")]
        public string Descripcion;

        [Required()]
        [Display(Name = "Baja")]
        public byte Id_Baja;
    }
    public class Beneficio_tipoMetadata
    {
        [Display(Name = "Tipo Beneficio")]
        public byte Id_BeneficioTipo;

        [Display(Name = "Tipo Beneficio")]
        public string Nombre;
    }
    public class Contacto_medioMetadata
    {
        [Display(Name = "Contacto Medio")]
        public byte Id_ContactoMedio;

        [Required()]
        [Display(Name = "Contacto Medio")]
        public string Nombre;  
    }
    public class CuentasMetadata
    {
        [Display(Name = "Cuenta")]
        public int Id_Cuenta;

        [Display(Name = "Login")]
        public int Id_Login;

        [Display(Name = "Perfil")]
        public short Id_Perfil;

        [Display(Name = "FechaCreacion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]        
        [DataType(DataType.DateTime)]
        public System.DateTime FechaCreacion;

        [Display(Name = "Fecha Modificación")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]        
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> FechaModificacion;

        [Display(Name = "Intentos Conexión")]
        public byte IntentosCnn;

        [Display(Name = "Inicio Bloqueo")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]        
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> InicioBloqueo;

        [Display(Name = "Sistema")]
        public byte Id_Sistema;

        [Required()]
        [Display(Name = "Activo")]
        public bool Estatus;

        [Display(Name = "Baja")]
        public byte Id_Baja;

        [Display(Name = "Registro Completo")]
        public bool RegistroCompleto;
    
    }
    public class DirectoriosMetadata
    {
        public byte Id_Directorio;
        [Required()]
        [Display(Name = "Nombre Directorio")]
        public string Nombre;

        [Required()]
        [Display(Name = "Tipo Directorio")]
        public byte Id_DirectorioTipo;

        [Required()]
        [Display(Name = "Activo")]
        public bool Estatus;
    
    }
    public class EntidadesMetadata
    {
        [Display(Name = "Pais")]
        public short Id_Pais;

        [Display(Name = "Entidad")]
        public short Id_Entidad;
        
        [Required()]
        [Display(Name = "Entidad")]
        public string Nombre;
        
        [Required()]
        public string Abreviatura;    
    }
    public class Menu_categoriaMetadata
    {
        [Display(Name = "Categoria Menu")]
        public short Id_MenuCategoria;
        
        [Required()]
        [Display(Name = "Categoria Menu")]
        public string Nombre;

        [Required()]
        public byte Orden;
    }
    public class LogInMetadata
    {
        [Display(Name = "Login")]
        public int Id_Login;

        [Required()]        
        public string Usuario;

        public string Senha;
        public string Salt;
        public bool UsoSalt;

        [Required()]
        [Display(Name = "Persona")]
        public int Id_Persona;
    }
    public class PaginasMetadata
    {        
        [Display(Name = "Página")]
        public short Id_Pagina;

        [Required()]
        [Display(Name = "Página")]
        public string Nombre;

        [Required()]
        public string Titulo;

        [Required()]
        [Display(Name = "Sistema")]
        public byte Id_Sistema;
    }
    public class PermisosMetadata
    {
        [Display(Name = "Permiso")]
        public byte Id_Permiso;

        [Required()]
        [Display(Name = "Permiso")]
        public string Nombre;

        [Required()]
        [Display(Name = "Activo")]
        public bool Estatus;
    }
    public class LocationsMetadata
    {
        [Display(Name = "Ubicación")]
        public int LocationId;

        [Required()]
        [Display(Name = "Nombre Ubicación")]
        public string LocationName;

        [Required()]
        [Display(Name = "Latitud")]
        public double Latitude;

        [Required()]
        [Display(Name = "Longitud")]
        public double Longitude;
    }
    public class Equipos_tipoMetadata
    {
        [Display(Name = "Tipo Equipo")]
        public byte Id_EquipoTipo;

        [Required()]
        [Display(Name = "Tipo Equipo")]
        public string Nombre;
    }
    public class Permisos_Cuentas_x_ActividadesMetadata
    {
        [Display(Name = "Cuenta")]
        public int Id_Cuenta;

        [Display(Name = "Actividad")]
        public short Id_Actividad;
        
        [Display(Name = "Permiso")]
        public byte Id_Permiso;
    }

    public class PreguntasMetadata
    {
        [Display(Name = "Pregunta")]
        public byte Id_Pregunta;

        [Required()]
        [Display(Name = "Pregunta")]
        public string Pregunta;
    }
    public class Preguntas_x_LoginMetadata
    {
        [Display(Name = "Login")]
        public int Id_Login;

        [Display(Name = "Pregunta")]
        public byte Id_Pregunta;

        [Required()]
        public string Respuesta;
    }

    public class EventsMetadata
    {
        [Display(Name = "Cita")]
        public int id ;

        [Required()]
        [Display(Name = "Descripción")]
        public string text ;

        [Display(Name = "Fecha de inicio")]
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> start_date;

        [Display(Name = "Fecha de termino")]
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> end_date;
    }

    public class PersonasMetadata 
    {
        public int Id_Persona;
        [Required()]
        [Display(Name = "A. Paterno")]
        public string APaterno;
        
        [Required()]
        [Display(Name = "A. Materno")]
        public string AMaterno;

        [Required()]
        [Display(Name = "Nombre(s)")]
        public string Nombre;

        [Display(Name = "Fecha de creación")]
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public System.DateTime FechaCreacion;

        public string Email;

        [Display(Name = "Género")]                
        public byte Id_Genero;

        [Display(Name = "Fecha de Nacimiento")]                
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> FechaNacimiento;

        public string RFC;
        public string Homoclave;
        public string CURP;
        public string Foto;
        public bool Estatus;
    }
         
}
