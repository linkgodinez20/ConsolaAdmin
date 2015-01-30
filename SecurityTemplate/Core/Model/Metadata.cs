using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Security.Core.Model
{
    public class SistemasMetadata
    {        
        [Display(Name = "Id")]
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
        [Display(Name = "Id")]
        public short Id_AreaDeTrabajo;

        [Required()]
        [StringLength(64)]
        [Display(Name = "Área de Trabajo")]
        public string Nombre;

        [Required()]
        [Display(Name = "Sistema")]
        public byte Id_Sistema;
    }

    public class Baja_motivosMetadata
    {
         [Display(Name = "Id")]
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
        [Display(Name = "Id")]
        public byte Id_BeneficioTipo;

        [Display(Name = "Tipo Beneficio")]
        public string Nombre;
    }
    public class Contacto_medioMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_ContactoMedio;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(20)]
        public String Nombre;
    }
    public class CuentasMetadata
    {
        [Display(Name = "Id")]
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
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_Directorio;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(25)]
        public String Nombre;

        [Required(ErrorMessage = "El campo [Tipo de directorio] es obligatorio.")]
        [Display(Name = "Tipo de directorio")]
        public Byte Id_DirectorioTipo;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    
    }
    public class EntidadesMetadata
    {
        [Display(Name = "Pais")]
        public short Id_Pais;

        [Display(Name = "Id")]
        public short Id_Entidad;
        
        [Required()]
        [Display(Name = "Entidad")]
        public string Nombre;
        
        [Required()]
        public string Abreviatura;    
    }
    public class Menu_categoriaMetadata
    {
        [Display(Name = "Id")]
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

        [Display(Name = "Contraseña")]
        public string Senha;

        public string Salt;
        public bool UsoSalt;

        [Required()]
        [Display(Name = "Persona")]
        public int Id_Persona;
    }
    public class PaginasMetadata
    {        
        [Display(Name = "Id")]
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
        [Display(Name = "Id")]
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
        [Display(Name = "Id")]
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
        [Display(Name = "Id")]
        public byte Id_EquipoTipo;

        [Required()]
        [Display(Name = "Tipo Equipo")]
        public string Nombre;
    }
    public class Permisos_Cuentas_x_ActividadesMetadata
    {
        [Display(Name = "Id")]
        public int Id_Cuenta;

        [Display(Name = "Actividad")]
        public short Id_Actividad;
        
        [Display(Name = "Permiso")]
        public byte Id_Permiso;
    }

    public class PreguntasMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_Pregunta;

        [Required(ErrorMessage = "El campo [Pregunta] es obligatorio.")]
        [StringLength(64)]
        public String Pregunta;
    }
    public class Preguntas_x_LoginMetadata
    {
        [Display(Name = "Id")]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> start_date;

        [Display(Name = "Fecha de termino")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> end_date;
    }

    

    public class ActividadesMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Actividad;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(128)]
        public String Nombre;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;

        [Required(ErrorMessage = "El campo [Directorio] es obligatorio.")]
        [Display(Name = "Directorio")]
        public Byte Id_Directorio;

        [Required(ErrorMessage = "El campo [Sistema] es obligatorio.")]
        [Display(Name = "Sistema")]
        public Byte Id_Sistema;
    }

    public class BajaMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_Baja;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(15)]
        public String Nombre;
    }

    public class BeneficioMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_Beneficio;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(64)]
        public String Nombre;

        [Required(ErrorMessage = "El campo [tipo beneficio] es obligatorio.")]
        [Display(Name = "tipo beneficio")]
        public Byte Id_BeneficioTipo;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }

    public class ContactoMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int32 Id_Contacto;

        [Required(ErrorMessage = "El campo [Tipo de contacto] es obligatorio.")]
        [Display(Name = "Tipo de contacto")]
        public Byte Id_ContactoTipo;

        [Display(Name = "Contacto")]
        [Required(ErrorMessage = "El campo [Contacto] es obligatorio.")]
        [StringLength(128)]
        public String Contacto1;

        [Required(ErrorMessage = "El campo [Medio de contacto] es obligatorio.")]
        [Display(Name = "Medio de contacto")]
        public Byte Id_ContactoMedio;

        [StringLength(128)]
        public String Notas;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }

    public class Contacto_tipoMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_ContactoTipo;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(20)]
        public String Nombre;
    }

    public class DomiciliosMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int32 Id_Domicilio;

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo [Tipo de contacto] es obligatorio.")]
        public Byte Id_ContactoTipo;

        [Required(ErrorMessage = "El campo [Domicilio] es obligatorio.")]
        [StringLength(100)]
        public String Domicilio;

        [Display(Name = "No. Ext.")]
        [Required(ErrorMessage = "El campo [Número exterior] es obligatorio.")]
        [StringLength(10)]
        public String NumeroExterior;

        [Display(Name = "No. Int.")]
        [StringLength(10)]
        public String NumeroInterior;

        [Column(Order = 0), Key, ForeignKey("Municipios")]
        [Required(ErrorMessage = "El campo [País] es obligatorio.")]
        [Display(Name = "País")]
        public Int16 Id_Pais;

        [Column(Order = 1), Key, ForeignKey("Municipios")]
        [Required(ErrorMessage = "El campo [Entidad] es obligatorio.")]
        [Display(Name = "Entidad")]
        public Int16 Id_Entidad;

        [Column(Order = 2), Key, ForeignKey("Municipios")]
        [Required(ErrorMessage = "El campo [Municipio] es obligatorio.")]
        [Display(Name = "Municipio")]
        public Int16? Id_Municipio;

        [Required(ErrorMessage = "El campo [Colonia] es obligatorio.")]
        [StringLength(100)]
        public String Colonia;

        [Display(Name = "Entre calle")]
        [StringLength(64)]
        public String EntreCalle;

        [Display(Name = "Y calle")]
        [StringLength(64)]
        public String YCalle;

        [StringLength(128)]
        public String Notas;

        [Display(Name = "Fecha de modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaModificacion;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }

    public class EquiposMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Equipo;

        [Display(Name = "Nombre del equipo")]
        public String Hostname;

        [StringLength(50)]
        public String IP;

        [StringLength(50)]
        public String MacAddress;

        [Required(ErrorMessage = "El campo [Tipo de equipo] es obligatorio.")]
        [Display(Name = "Tipo de equipo")]
        public Byte Id_EquipoTipo;

        [Display(Name = "Descripción")]
        [StringLength(128)]
        public String Descripcion;

        [Display(Name = "Fecha de alta")]
        public DateTime FechaAlta;

        [Display(Name = "Fecha de caducidad")]
        public DateTime? FechaCaducidad;

        [Required(ErrorMessage = "El campo [Cuenta] es obligatorio.")]
        [Display(Name = "Cuenta")]
        public Int32 Id_Cuenta;

        [Required(ErrorMessage = "El campo [Sistema] es obligatorio.")]
        [Display(Name = "Sistema")]
        public Byte Id_Sistema;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }
    public class GeneroMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Byte Id_Genero;

        [StringLength(25)]
        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        public String Nombre;
    }

    public class MenuMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Menu;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(50)]
        public String Nombre;

        [Required(ErrorMessage = "El campo [Padre] es obligatorio.")]
        [Display(Name = "Padre")]
        public Int16 Parent;

        [StringLength(64)]
        public String Tooltip;

        [Required(ErrorMessage = "El campo [Página] es obligatorio.")]
        [Display(Name = "Página")]
        public Int16 Id_Pagina;

        [Required(ErrorMessage = "El campo [Orden] es obligatorio.")]
        [Display(Name = "Orden")]
        public Byte Orden;

        [Required(ErrorMessage = "El campo [Habilitado] es obligatorio.")]
        [Display(Name = "Habilitado")]
        public Boolean Habilitado;

        [Required(ErrorMessage = "El campo [Categoría] es obligatorio.")]
        [Display(Name = "Categoría")]
        public Int16 Id_MenuCategoria;

        [Required(ErrorMessage = "El campo [Sistema] es obligatorio.")]
        [Display(Name = "Sistema")]
        public Byte Id_Sistema;
    }

    public class MunicipiosMetadata
    {
        [Key, Column(Order = 0)]
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Pais;

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "El campo [Entidad] es obligatorio.")]
        [Display(Name = "Entidad")]
        public Int16 Id_Entidad;

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "El campo [Municipio] es obligatorio.")]
        [Display(Name = "Municipio")]
        public Int16 Id_Municipio;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(64)]
        [Display(Name = "Municipio")]
        public String Nombre;

        [StringLength(10)]
        public String Abreviatura;
    }

    public class PaisesMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Pais;

        [Required(ErrorMessage = "El campo [FIPS] es obligatorio.")]
        [StringLength(10)]
        public String FIPS;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(64)]
        public String Nombre;

        [Required(ErrorMessage = "El campo [Prioridad] es obligatorio.")]
        public Byte Prioridad;
    }

    public class PerfilesMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int16 Id_Perfil;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(64)]
        public String Nombre;

        [Display(Name = "Función")]
        [StringLength(256)]
        public String Funcion;

        [StringLength(5)]
        public String Tipo;

        [Required(ErrorMessage = "El campo [Nivel] es obligatorio.")]
        [StringLength(5)]
        public String Nivel;

        [Required(ErrorMessage = "El campo [Sistema] es obligatorio.")]
        [Display(Name = "Sistema")]
        public Byte Id_Sistema;
    }

    public class PersonasMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int32 Id_Persona;

        [Required(ErrorMessage = "El campo [Ap. Paterno] es obligatorio.")]
        [Display(Name = "Ap. Paterno")]
        [StringLength(50)]
        public String APaterno;

        [Display(Name = "Ap. Materno")]
        [StringLength(50)]
        public String AMaterno;

        [Required(ErrorMessage = "El campo [Nombre] es obligatorio.")]
        [StringLength(50)]
        public String Nombre;

        [Display(Name = "Fecha de creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        public DateTime FechaCreacion;

        [Required(ErrorMessage = "El campo [E-mail] es obligatorio.")]
        [Display(Name = "E-mail")]
        [StringLength(64)]
        public String Email;

        [Display(Name = "Género")]
        [Required(ErrorMessage = "El campo [Género] es obligatorio.")]
        public Byte Id_Genero;

        
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaNacimiento;

        [StringLength(10)]
        public String RFC;

        [StringLength(3)]
        public String Homoclave;

        [StringLength(18)]
        public String CURP;

        public String Foto;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }

    public class SesionesMetadata
    {
        [Display(Name = "Id")]
        [ScaffoldColumn(false)]
        public Int32 Id_Sesion;

        [ScaffoldColumn(false)]
        public System.Guid Identificador;

        [Required(ErrorMessage = "El campo [Cuenta] es obligatorio.")]
        [Display(Name = "Cuenta")]
        public Int32 Id_Cuenta;

        [Required(ErrorMessage = "El campo [Fecha/Hora inicio] es obligatorio.")]
        [Display(Name = "Fecha/Hora inicio")]
        public System.DateTime FechaHoraInicio;

        [Display(Name = "En línea")]
        public Boolean OnLine;

        [Display(Name = "Sesión cerrada")]
        public Boolean CierreSesion;

        [Display(Name = "Último movimiento")]
        public Nullable<System.DateTime> UltimoMovimiento;

        [Required(ErrorMessage = "El campo [Sistema] es obligatorio.")]
        [Display(Name = "Sistema")]
        public Byte Id_Sistema;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio.")]
        public Boolean Estatus;
    }
         
}
