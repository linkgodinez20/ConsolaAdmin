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
    
    public partial class Directorios
    {
        public Directorios()
        {
            this.Actividades = new HashSet<Actividades>();
            this.Perfiles = new HashSet<Perfiles>();
            this.Sistemas = new HashSet<Sistemas>();
        }
    
        public byte Id_Directorio { get; set; }
        public string Nombre { get; set; }
        public byte Id_DirectorioTipo { get; set; }
        public bool Estatus { get; set; }
    
        public virtual ICollection<Actividades> Actividades { get; set; }
        public virtual Directorio_tipo Directorio_tipo { get; set; }
        public virtual ICollection<Perfiles> Perfiles { get; set; }
        public virtual ICollection<Sistemas> Sistemas { get; set; }
    }
}
