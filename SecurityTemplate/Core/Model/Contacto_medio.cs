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
    
    public partial class Contacto_medio
    {
        public Contacto_medio()
        {
            this.Contacto = new HashSet<Contacto>();
        }
    
        public byte Id_ContactoMedio { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Contacto> Contacto { get; set; }
    }
}
