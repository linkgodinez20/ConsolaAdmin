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
    
    public partial class Entidades
    {
        public Entidades()
        {
            this.Municipios = new HashSet<Municipios>();
        }
    
        public short Id_Pais { get; set; }
        public short Id_Entidad { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    
        public virtual Paises Paises { get; set; }
        public virtual ICollection<Municipios> Municipios { get; set; }
    }
}
