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
    
    public partial class Contacto_x_Persona
    {
        public int Id { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Contacto { get; set; }
    
        public virtual Contactos Contactos { get; set; }
        public virtual Personas Personas { get; set; }
    }
}