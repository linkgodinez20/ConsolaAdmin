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
    
    public partial class Perfiles
    {
        public Perfiles()
        {
            this.AreasDeTrabajo_x_Perfiles = new HashSet<AreasDeTrabajo_x_Perfiles>();
            this.Beneficio_x_Perfiles = new HashSet<Beneficio_x_Perfiles>();
            this.Cuentas = new HashSet<Cuentas>();
            this.Directorios_x_Perfiles = new HashSet<Directorios_x_Perfiles>();
            this.ParametrosGpo_x_Perfiles = new HashSet<ParametrosGpo_x_Perfiles>();
            this.Perfil_x_Controlador_x_Accion = new HashSet<Perfil_x_Controlador_x_Accion>();
        }
    
        public short Id_Perfil { get; set; }
        public string Nombre { get; set; }
        public string Funcion { get; set; }
        public string Tipo { get; set; }
        public string Nivel { get; set; }
        public byte Id_Sistema { get; set; }
    
        public virtual ICollection<AreasDeTrabajo_x_Perfiles> AreasDeTrabajo_x_Perfiles { get; set; }
        public virtual ICollection<Beneficio_x_Perfiles> Beneficio_x_Perfiles { get; set; }
        public virtual ICollection<Cuentas> Cuentas { get; set; }
        public virtual ICollection<Directorios_x_Perfiles> Directorios_x_Perfiles { get; set; }
        public virtual ICollection<ParametrosGpo_x_Perfiles> ParametrosGpo_x_Perfiles { get; set; }
        public virtual ICollection<Perfil_x_Controlador_x_Accion> Perfil_x_Controlador_x_Accion { get; set; }
        public virtual Sistemas Sistemas { get; set; }
    }
}
