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
    
    public partial class Domicilios
    {
        public Domicilios()
        {
            this.Contacto = new HashSet<Contacto>();
            this.Personas = new HashSet<Personas>();
        }
    
        public int Id_Domicilio { get; set; }
        public byte Id_ContactoTipo { get; set; }
        public string Domicilio { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public short Id_Pais { get; set; }
        public short Id_Entidad { get; set; }
        public Nullable<short> Id_Municipio { get; set; }
        public string Colonia { get; set; }
        public string EntreCalle { get; set; }
        public string YCalle { get; set; }
        public string Notas { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public bool Estatus { get; set; }
    
        public virtual Contacto_tipo Contacto_tipo { get; set; }
        public virtual Municipios Municipios { get; set; }
        public virtual ICollection<Contacto> Contacto { get; set; }
        public virtual ICollection<Personas> Personas { get; set; }
    }
}
