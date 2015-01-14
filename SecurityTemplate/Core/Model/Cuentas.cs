//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Security.Core.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cuentas
    {
        public Cuentas()
        {
            this.Benficio_x_Cuenta = new HashSet<Benficio_x_Cuenta>();
            this.Equipos = new HashSet<Equipos>();
            this.Permisos_AreaDeTrabajo_x_Cuenta = new HashSet<Permisos_AreaDeTrabajo_x_Cuenta>();
            this.Permisos_Cuentas_x_Actividades = new HashSet<Permisos_Cuentas_x_Actividades>();
            this.Sesiones = new HashSet<Sesiones>();
            this.Personas = new HashSet<Personas>();
            this.Baja_motivos = new HashSet<Baja_motivos>();
        }
    
        public int Id_Cuenta { get; set; }
        public int Id_Login { get; set; }
        public short Id_Perfil { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public byte IntentosCnn { get; set; }
        public Nullable<System.DateTime> InicioBloqueo { get; set; }
        public byte Id_Sistema { get; set; }
        public bool Estatus { get; set; }
        public byte Id_Baja { get; set; }
        public bool RegistroCompleto { get; set; }
    
        public virtual Baja Baja { get; set; }
        public virtual ICollection<Benficio_x_Cuenta> Benficio_x_Cuenta { get; set; }
        public virtual LogIn LogIn { get; set; }
        public virtual Perfiles Perfiles { get; set; }
        public virtual Sistemas Sistemas { get; set; }
        public virtual ICollection<Equipos> Equipos { get; set; }
        public virtual ICollection<Permisos_AreaDeTrabajo_x_Cuenta> Permisos_AreaDeTrabajo_x_Cuenta { get; set; }
        public virtual ICollection<Permisos_Cuentas_x_Actividades> Permisos_Cuentas_x_Actividades { get; set; }
        public virtual ICollection<Sesiones> Sesiones { get; set; }
        public virtual ICollection<Personas> Personas { get; set; }
        public virtual ICollection<Baja_motivos> Baja_motivos { get; set; }
    }
}