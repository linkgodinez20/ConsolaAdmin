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
    
    public partial class Equipos_tipo
    {
        public Equipos_tipo()
        {
            this.Equipos = new HashSet<Equipos>();
        }
    
        public byte Id_EquipoTipo { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Equipos> Equipos { get; set; }
    }
}
